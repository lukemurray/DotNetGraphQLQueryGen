using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DotNetGqlClient
{
    public abstract class BaseGraphQLClient
    {
        private uint argNum = 0;
        protected Dictionary<string, string> typeMappings;

        protected QueryRequest MakeQuery<TSchema, TQuery>(Expression<Func<TSchema, TQuery>> query, bool mutation = false)
        {
            argNum = 0;
            var args = new List<string>();
            var gql = new StringBuilder();
            var variables = new Dictionary<string, object>();

            if (query.NodeType != ExpressionType.Lambda)
                throw new ArgumentException($"Must provide a LambdaExpression", "query");
            var lambda = (LambdaExpression)query;

            if (lambda.Body.NodeType != ExpressionType.New && lambda.Body.NodeType != ExpressionType.MemberInit)
                throw new ArgumentException($"LambdaExpression must return a NewExpression or MemberInitExpression");

            GetObjectSelection(gql, lambda.Body, args, variables);

            // prepend operationname as it may have arguments
            gql.Insert(0, $"{(mutation ? "mutation" : "query")} BaseGraphQLClient{(args.Any() ? $"({string.Join(",", args)})" : "")} {{\n");

            gql.Append(@"}");
            return new QueryRequest
            {
                Query = gql.ToString(),
                Variables = variables
            };
        }

        private void GetObjectSelection(StringBuilder gql, Expression exp, List<string> args, Dictionary<string, object> variables)
        {
            if (exp.NodeType == ExpressionType.New)
            {
                var newExp = (NewExpression)exp;
                for (int i = 0; i < newExp.Arguments.Count; i++)
                {
                    var fieldVal = newExp.Arguments[i];
                    var fieldProp = newExp.Members[i];
                    gql.AppendLine($"{fieldProp.Name}: {GetFieldSelection(fieldVal, args, variables)}");
                }
            }
            else
            {
                var mi = (MemberInitExpression)exp;
                for (int i = 0; i < mi.Bindings.Count; i++)
                {
                    var valExp = ((MemberAssignment)mi.Bindings[i]).Expression;
                    var fieldVal = mi.Bindings[i].Member;
                    gql.AppendLine($"{fieldVal.Name}: {GetFieldSelection(valExp, args, variables)}");
                }
            }
        }

        private string GetFieldSelection(Expression field, List<string> args, Dictionary<string, object> variables)
        {
            if (field.NodeType == ExpressionType.MemberAccess)
            {
                var memberExp = (MemberExpression)field;
                // we only support 1 level field selection as we are just generating gql not doing post processing
                // e.g. client.MakeQuery(q => new
                // {
                //     Movie = q.Movies(s => new
                //     {
                //         s.Rating.Value,
                //         s.Director().Died
                //     }),
                // });
                // both of those selections are invalid. You just selection s.Rating and the return value type is float?
                // and for the director died date you select it like gql s.Director(d => new { d.Died })
                // TODO we could generate s.Director().Died into the line above
                if (memberExp.Expression.NodeType != ExpressionType.Parameter)
                    throw new ArgumentException("It looks like you are make a deep property call. We only support a single depth to generate GQL. You can use the methods to select nest objects");
                var member = memberExp.Member;
                var attribute = member.GetCustomAttributes(typeof(GqlFieldNameAttribute)).Cast<GqlFieldNameAttribute>().FirstOrDefault();
                if (attribute != null)
                    return attribute.Name;
                return member.Name;
            }
            else if (field.NodeType == ExpressionType.Call)
            {
                var call = (MethodCallExpression)field;
                return GetSelectionFromMethod(call, args, variables);
            }
            else if (field.NodeType == ExpressionType.Quote)
            {
                return GetFieldSelection(((UnaryExpression)field).Operand, args, variables);
            }
            else
            {
                throw new ArgumentException($"Field expression should be a call or member access expression", "field");
            }
        }

        private string GetSelectionFromMethod(MethodCallExpression call, List<string> operationArgs, Dictionary<string, object> variables)
        {
            var select = new StringBuilder();

            var attribute = call.Method.GetCustomAttributes(typeof(GqlFieldNameAttribute)).Cast<GqlFieldNameAttribute>().FirstOrDefault();
            if (attribute != null)
                select.Append(attribute.Name);
            else
                select.Append(call.Method.Name);

            if (call.Arguments.Count > 1)
            {
                var argVals = new List<string>();
                for (int i = 0; i < call.Arguments.Count - 1; i++)
                {
                    var arg = call.Arguments.ElementAt(i);
                    var param = call.Method.GetParameters().ElementAt(i);
                    Type argType = null;
                    object argVal = null;

                    if (arg.NodeType == ExpressionType.Convert)
                    {
                        arg = ((UnaryExpression)arg).Operand;
                    }

                    switch (arg.NodeType)
                    {
                        case ExpressionType.Constant:
                            var constArg = (ConstantExpression)arg;
                            argType = constArg.Type;
                            argVal = constArg.Value;
                            break;

                        case ExpressionType.MemberAccess:
                            var ma = (MemberExpression)arg;
                            var ce = (ConstantExpression)ma.Expression;
                            argType = ma.Type;
                            argVal = ma.Member.MemberType == MemberTypes.Field
                                ? ((FieldInfo)ma.Member).GetValue(ce.Value)
                                : ((PropertyInfo)ma.Member).GetValue(ce.Value);
                            break;
                        case ExpressionType.New:
                        case ExpressionType.NewArrayInit:
                            argVal = Expression.Lambda(arg).Compile().DynamicInvoke();
                            argType = argVal.GetType();
                            break;
                        default:
                            throw new Exception($"Unsupported argument type {arg.NodeType}");
                    }

                    if (argVal == null)
                        continue;

                    argVals.Add($"{param.Name}: {ArgValToString(operationArgs, variables, param, argType, argVal)}");
                    ;
                };
                if (argVals.Any())
                    select.Append($"({string.Join(", ", argVals)})");
            }
            select.Append(" {" + Environment.NewLine);
            if (call.Arguments.Count == 0)
            {
                if (call.Method.ReturnType.IsEnumerableOrArray())
                {
                    select.Append(GetDefaultSelection(call.Method.ReturnType.GetGenericArguments().First()));
                }
                else
                {
                    select.Append(GetDefaultSelection(call.Method.ReturnType));
                }
            }
            else
            {
                var exp = call.Arguments.Last();
                if (exp.NodeType == ExpressionType.Quote)
                    exp = ((UnaryExpression)exp).Operand;
                if (exp.NodeType == ExpressionType.Lambda)
                    exp = ((LambdaExpression)exp).Body;
                GetObjectSelection(select, exp, operationArgs, variables);
            }
            select.Append("}");
            return select.ToString();
        }

        private string ArgValToString(List<string> operationArgs, Dictionary<string, object> variables, ParameterInfo param, Type argType, object val)
        {
            var type = Nullable.GetUnderlyingType(argType) ?? argType;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime: return $"\"{(DateTime)val:o}\"";
                case TypeCode.String: return $"\"{val}\"";
                case TypeCode.Double: return ((double)val).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Decimal: return ((decimal)val).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Single: return ((float)val).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Boolean: return val.ToString().ToLower();
                default:
                    if (type == typeof(Guid))
                    {
                        return $"\"{val}\"";
                    }
                    if (type.IsEnumerableOrArray())
                    {
                        var argName = $"a{argNum++}";
                        var arrType = argType.GetEnumerableOrArrayType();
                        var isNullable = arrType.IsNullableType();
                        if (isNullable)
                            arrType = arrType.GetGenericArguments()[0];
                        if (!typeMappings.ContainsKey(arrType.Name))
                            throw new ArgumentException($"Can't find GQL type for Dotnet type '{arrType.Name}'");
                        operationArgs.Add($"${argName}: [{(isNullable ? typeMappings[arrType.Name].Trim('!') : typeMappings[arrType.Name])}]");
                        variables.Add($"{argName}", val);
                        return $"${argName}";
                    }
                    return val.ToString();
            }
        }

        private static string GetDefaultSelection(Type returnType)
        {
            var select = new StringBuilder();
            foreach (var field in returnType.GetProperties())
            {
                var name = field.Name;
                var attribute = field.GetCustomAttributes(typeof(GqlFieldNameAttribute)).Cast<GqlFieldNameAttribute>().FirstOrDefault();
                if (attribute != null)
                    name = attribute.Name;

                select.AppendLine($"{field.Name}: {name}");
            }
            return select.ToString();
        }
    }

    public class QueryRequest
    {
        // Name of the query or mutation you want to run in the Query (if it contains many)
        public string OperationName { get; set; }
        /// <summary>
        /// GraphQL query document
        /// </summary>
        /// <value></value>
        public string Query { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }

    public static class TypeExtensions
    {
        /// <summary>
        /// Returns true if this type is an Enumerable<> or an array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEnumerableOrArray(this Type source)
        {
            if (source == typeof(string) || source == typeof(byte[]))
                return false;

            if (source.GetTypeInfo().IsArray)
            {
                return true;
            }
            var isEnumerable = false;
            if (source.GetTypeInfo().IsGenericType && !source.IsNullableType())
            {
                isEnumerable = IsGenericTypeEnumerable(source);
            }
            return isEnumerable;
        }

        private static bool IsGenericTypeEnumerable(Type source)
        {
            bool isEnumerable = (source.GetTypeInfo().IsGenericType && source.GetGenericTypeDefinition() == typeof(IEnumerable<>) || source.GetTypeInfo().IsGenericType && source.GetGenericTypeDefinition() == typeof(IQueryable<>));
            if (!isEnumerable)
            {
                foreach (var intType in source.GetInterfaces())
                {
                    isEnumerable = IsGenericTypeEnumerable(intType);
                    if (isEnumerable)
                        break;
                }
            }

            return isEnumerable;
        }

        /// <summary>
        /// Return the array element type or the generic type for a IEnumerable<T>
        /// Specifically does not treat string as IEnumerable<char> and will not return byte for byte[]
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetEnumerableOrArrayType(this Type type)
        {
            if (type == typeof(string) || type == typeof(byte[]) || type == typeof(byte))
            {
                return null;
            }
            if (type.IsArray)
            {
                return type.GetElementType();
            }
            if (type.GetTypeInfo().IsGenericType && (
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                type.GetGenericTypeDefinition() == typeof(IList<>) ||
                type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) ||
                type.GetGenericTypeDefinition() == typeof(List<>) ||
                type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)))
            {
                return type.GetGenericArguments()[0];
            }
            foreach (var intType in type.GetInterfaces())
            {
                if (intType.IsEnumerableOrArray())
                {
                    return intType.GetEnumerableOrArrayType();
                }
            }
            return null;
        }

        public static bool IsNullableType(this Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}