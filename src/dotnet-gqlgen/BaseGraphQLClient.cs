using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DotNetGqlClient
{
    public abstract class BaseGraphQLClient
    {
        protected string MakeQuery<TSchema, TQuery>(Expression<Func<TSchema, TQuery>> query, bool mutation = false)
        {
            var gql = new StringBuilder();
            gql.AppendLine($"{(mutation ? "mutation" : "query")} BaseGraphQLClient {{");

            if (query.NodeType != ExpressionType.Lambda)
                throw new ArgumentException($"Must provide a LambdaExpression", "query");
            var lambda = (LambdaExpression)query;

            if (lambda.Body.NodeType != ExpressionType.New && lambda.Body.NodeType != ExpressionType.MemberInit)
                throw new ArgumentException($"LambdaExpression must return a NewExpression or MemberInitExpression");

            GetObjectSelection(gql, lambda.Body);

            gql.Append(@"}");
            return gql.ToString();
        }

        private static void GetObjectSelection(StringBuilder gql, Expression exp)
        {
            if (exp.NodeType == ExpressionType.New)
            {
                var newExp = (NewExpression)exp;
                for (int i = 0; i < newExp.Arguments.Count; i++)
                {
                    var fieldVal = newExp.Arguments[i];
                    var fieldProp = newExp.Members[i];
                    gql.AppendLine($"{fieldProp.Name}: {GetFieldSelection(fieldVal)}");
                }
            }
            else
            {
                var mi = (MemberInitExpression)exp;
                for (int i = 0; i < mi.Bindings.Count; i++)
                {
                    var valExp = ((MemberAssignment)mi.Bindings[i]).Expression;
                    var fieldVal = mi.Bindings[i].Member;
                    gql.AppendLine($"{fieldVal.Name}: {GetFieldSelection(valExp)}");
                }
            }
        }

        private static string GetFieldSelection(Expression field)
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
                return GetSelectionFromMethod(call);
            }
            else if (field.NodeType == ExpressionType.Quote)
            {
                return GetFieldSelection(((UnaryExpression)field).Operand);
            }
            else
            {
                throw new ArgumentException($"Field expression should be a call or member access expression", "field");
            }
        }

        private static string GetSelectionFromMethod(MethodCallExpression call)
        {
            var select = new StringBuilder();

            var attribute = call.Method.GetCustomAttributes(typeof(GqlFieldNameAttribute)).Cast<GqlFieldNameAttribute>().FirstOrDefault();
            if (attribute != null)
                select.Append(attribute.Name);
            else
                select.Append(call.Method.Name);

            if (call.Arguments.Count > 1)
            {
                var argVals = new List<object>();
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

                    if (arg.NodeType == ExpressionType.Constant)
                    {
                        var constArg = (ConstantExpression)arg;
                        argType = constArg.Type;
                        argVal = constArg.Value;
                    }
                    else if (arg.NodeType == ExpressionType.MemberAccess)
                    {
                        var ma = (MemberExpression)arg;
                        var ce = (ConstantExpression)ma.Expression;
                        argType = ma.Type;
                        if (ma.Member.MemberType == MemberTypes.Field)
                            argVal = ((FieldInfo)ma.Member).GetValue(ce.Value);
                        else
                            argVal = ((PropertyInfo)ma.Member).GetValue(ce.Value);
                    }
                    else if (arg.NodeType == ExpressionType.New)
                    {
                        argVal = Expression.Lambda(arg).Compile().DynamicInvoke();
                        argType = argVal.GetType();
                    }

                    else
                    {
                        throw new Exception($"Unsupported argument type {arg.NodeType}");
                    }
                    if (argVal == null)
                        continue;
                    if (argType == typeof(string) || argType == typeof(Guid) || argType == typeof(Guid?))
                    {
                        argVals.Add($"{param.Name}: \"{argVal}\"");
                    }
                    else if (argType == typeof(DateTime) || argType == typeof(DateTime?))
                    {
                        argVals.Add($"{param.Name}: \"{((DateTime)argVal).ToString("o")}\"");
                    }
                    else
                    {
                        argVals.Add($"{param.Name}: {argVal}");
                    }
                };
                if (argVals.Any())
                    select.Append($"({string.Join(", ", argVals)})");
            }
            select.Append(" {\n");
            if (call.Arguments.Count == 0)
            {
                if (call.Method.ReturnType.GetInterfaces().Select(i => i.GetTypeInfo().GetGenericTypeDefinition()).Contains(typeof(IEnumerable<>)))
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
                GetObjectSelection(select, exp);
            }
            select.Append("}");
            return select.ToString();
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
}