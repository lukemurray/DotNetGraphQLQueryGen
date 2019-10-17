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
                    gql.AppendLine($"{mi.Bindings[i].Member.Name}: {GetFieldSelection(valExp)}");
                }
            }
        }

        private static string GetFieldSelection(Expression field)
        {
            if (field.NodeType == ExpressionType.MemberAccess)
            {
                var member = ((MemberExpression)field).Member;
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
                    var constArg = (ConstantExpression)arg;
                    if (constArg.Value == null)
                        continue;
                    if (constArg.Type == typeof(string) || constArg.Type == typeof(Guid))
                    {
                        argVals.Add($"{param.Name}: \"{constArg.Value}\"");
                    }
                    else
                    {
                        argVals.Add($"{param.Name}: {constArg.Value}");
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