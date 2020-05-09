using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace DotNetGqlClient
{
    public abstract class BaseGraphQLClient
    {
        private uint argNum = 0;
        protected Dictionary<string, string> typeMappings;

        private class MemberInitDictionary : Dictionary<string, (object, Type)> { }

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

            foreach (var selections in GetObjectSelection(lambda.Body, args, variables))
            {
                gql.AppendLine(selections);
            }

            // prepend operationname as it may have arguments
            gql.Insert(0, $"{(mutation ? "mutation" : "query")} BaseGraphQLClient{(args.Any() ? $"({string.Join(",", args)})" : "")} {{\n");

            gql.Append(@"}");
            return new QueryRequest
            {
                Query = gql.ToString(),
                Variables = variables
            };
        }

        private IEnumerable<string> GetObjectSelection(Expression exp, List<string> args, Dictionary<string, object> variables)
        {
            if (exp.NodeType == ExpressionType.New)
            {
                var newExp = (NewExpression)exp;
                for (int i = 0; i < newExp.Arguments.Count; i++)
                {
                    var fieldVal = newExp.Arguments[i];
                    var fieldProp = newExp.Members[i];
                    yield return $"{fieldProp.Name}: {GetFieldSelection(fieldVal, args, variables)}";
                }
            }
            else if (exp.NodeType == ExpressionType.MemberInit)
            {
                var mi = (MemberInitExpression)exp;
                for (int i = 0; i < mi.Bindings.Count; i++)
                {
                    var valExp = ((MemberAssignment)mi.Bindings[i]).Expression;
                    var fieldVal = mi.Bindings[i].Member;
                    yield return $"{fieldVal.Name}: {GetFieldSelection(valExp, args, variables)}";
                }
            }
            else
            {
                throw new ArgumentException($"Selection {exp.NodeType} \"{exp}\" must be a NewExpression or MemberInitExpression");
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


            IEnumerable<string> selection;
            if (call.Arguments.Count == 0)
            {
                selection = (call.Method.ReturnType.IsEnumerableOrArray()
                    ? GetDefaultSelection(call.Method.ReturnType.GetGenericArguments().First())
                    : GetDefaultSelection(call.Method.ReturnType)).ToList();
            }
            else
            {
                var selectorExp = call.Arguments.Last();

                if (selectorExp.NodeType == ExpressionType.Quote)
                    selectorExp = ((UnaryExpression)selectorExp).Operand;

                if (selectorExp.NodeType == ExpressionType.Lambda)
                    selectorExp = ((LambdaExpression)selectorExp).Body;
                else
                    selectorExp = null; // supposed scalar awaited as return


                var argVals = new List<string>();
                var parameters = call.Method.GetParameters();
                var arguments = call.Arguments;

                var count = selectorExp != null
                    ? arguments.Count - 1
                    : arguments.Count;

                for (int i = 0; i < count; i++)
                {
                    var arg = call.Arguments[i];
                    var param = parameters.ElementAt(i);

                    (object argVal, Type argType) = ArgToTypeAndValue(arg);
                    if (argVal == null)
                        continue;

                    argVals.Add($"{param.Name}: {ArgValToString(operationArgs, variables, param, argType, argVal)}");
                };

                if (argVals.Count > 0)
                    select.Append($"({string.Join(", ", argVals)})");

                selection = selectorExp != null
                    ? GetObjectSelection(selectorExp, operationArgs, variables)
                    : Enumerable.Empty<string>();
            }

            var selectionArray = selection.ToArray();
            if (selectionArray.Length > 0)
            {
                select.Append(" {" + Environment.NewLine);
                foreach (var line in selectionArray)
                {
                    select.AppendLine(line);
                }
                select.Append("}");
            }

            return select.ToString();
        }

        private (object argVal, Type argType) ArgToTypeAndValue(Expression arg)
        {
            Type argType;
            object argVal;

            switch (arg.NodeType)
            {
                case ExpressionType.Convert:
                    return ArgToTypeAndValue(((UnaryExpression)arg).Operand);
                case ExpressionType.Constant:
                    var constArg = (ConstantExpression)arg;
                    argType = constArg.Type;
                    argVal = constArg.Value;
                    if (argType.IsEnum)
                    {
                        var attr = arg.Type.GetField(Enum.GetName(arg.Type, constArg.Value)).GetCustomAttributes<GqlFieldNameAttribute>().FirstOrDefault();
                        argVal = attr?.Name ?? argVal;
                    }
                    break;
                case ExpressionType.MemberAccess when ((MemberExpression)arg).Expression is ConstantExpression ce:
                    var mac = (MemberExpression)arg;
                    argType = mac.Type;
                    argVal = mac.Member.MemberType == MemberTypes.Field
                        ? ((FieldInfo)mac.Member).GetValue(ce.Value)
                        : ((PropertyInfo)mac.Member).GetValue(ce.Value);
                    break;
                case ExpressionType.MemberAccess when ((MemberExpression)arg).Expression.GetType().Name == "FieldExpression":
                    var maf = (MemberExpression)arg;
                    var fieldName = maf.Member.Name;
                    (object fieldVal, Type fieldType) = ArgToTypeAndValue(maf.Expression);
                    if (maf.Member.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fi = fieldType.GetField(fieldName);
                        argType = fi.FieldType;
                        argVal = fi.GetValue(fieldVal);
                    }
                    else
                    {
                        PropertyInfo pi = fieldType.GetProperty(fieldName);
                        argType = pi.PropertyType;
                        argVal = pi.GetValue(fieldVal);
                    }
                    break;
                case ExpressionType.MemberInit:
                    var memberInitDict = new MemberInitDictionary();
                    foreach (var binding in ((MemberInitExpression)arg).Bindings)
                    {
                        var attr = binding.Member.GetCustomAttributes<GqlFieldNameAttribute>().FirstOrDefault();
                        var name = attr?.Name ?? binding.Member.Name;
                        var expr = ((MemberAssignment)binding).Expression;
                        memberInitDict.Add(name, ArgToTypeAndValue(expr));
                    }
                    argVal = memberInitDict;
                    argType = argVal.GetType();
                    break;
                case ExpressionType.New:
                case ExpressionType.NewArrayInit:
                case ExpressionType.ListInit:
                    argVal = Expression.Lambda(arg).Compile().DynamicInvoke();
                    argType = argVal.GetType();
                    break;
                default:
                    throw new Exception($"Unsupported argument type {arg.NodeType}");
            }

            return (argVal, argType);
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
                    if (type == typeof(MemberInitDictionary))
                    {
                        var memberInitDict = (MemberInitDictionary)val;
                        var memberFields = memberInitDict.Select((a) => $"{a.Key}: {ArgValToString(operationArgs, variables, param, a.Value.Item2, a.Value.Item1)}");
                        return $"{{ {string.Join(", ", memberFields)} }}";
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

        private static IEnumerable<string> GetDefaultSelection(Type returnType)
        {
            foreach (var field in returnType.GetProperties())
            {
                var name = field.Name;
                var attribute = field.GetCustomAttributes(typeof(GqlFieldNameAttribute)).Cast<GqlFieldNameAttribute>().FirstOrDefault();
                if (attribute != null)
                    name = attribute.Name;

                yield return $"{field.Name}: {name}";
            }
        }

    }

    public class QueryRequest
    {
        // Name of the query or mutation you want to run in the Query (if it contains many)
        [JsonProperty("operationName")]
        public string OperationName { get; set; }
        /// <summary>
        /// GraphQL query document
        /// </summary>
        /// <value></value>
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("variables")]
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
