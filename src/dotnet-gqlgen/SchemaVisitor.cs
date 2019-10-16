using System;
using System.Collections.Generic;
using System.Linq;
using GraphQLSchema.Grammer;

namespace dotnet_gqlgen
{
    internal class SchemaVisitor : GraphQLSchemaBaseVisitor<object>
    {
        private readonly SchemaInfo schemaInfo;
        private List<Field> addFieldsTo;

        public SchemaInfo SchemaInfo => schemaInfo;

        public SchemaVisitor(Dictionary<string, string> typeMappings)
        {
            this.schemaInfo = new SchemaInfo(typeMappings);
        }

        public override object VisitFieldDef(GraphQLSchemaParser.FieldDefContext context)
        {
            var result = base.VisitFieldDef(context);
            var docComment = context.comment().LastOrDefault();
            var desc = docComment != null ? (string)VisitComment(docComment) : null;
            var name = context.name.Text;
            var args = (List<Arg>)VisitArguments(context.args);
            var type = context.type.GetText();
            var isArray = type[0] == '[';
            type = type.Trim('[', ']');
            addFieldsTo.Add(new Field(this.schemaInfo)
            {
                Name = name,
                TypeName = type,
                IsArray = isArray,
                Args = args,
                Description = desc,
            });
            return result;
        }

        public override object VisitArguments(GraphQLSchemaParser.ArgumentsContext context)
        {
            var args = new List<Arg>();
            if (context != null)
            {
                foreach (var arg in context.argument())
                {
                    var type = arg.dataType().GetText();
                    var isArray = type[0] == '[';
                    type = type.Trim('[', ']');
                    args.Add(new Arg(this.schemaInfo)
                    {
                        Name = arg.NAME().GetText(),
                        TypeName = type,
                        Required = arg.required != null,
                        IsArray = isArray
                    });
                }
            }
            return args;
        }

        internal void SetFieldConsumer(List<Field> item)
        {
            this.addFieldsTo = item;
        }

        public override object VisitComment(GraphQLSchemaParser.CommentContext context)
        {
            return context.GetText().Trim('"', ' ', '\t', '\n', '\r');
        }

        public override object VisitSchemaDef(GraphQLSchemaParser.SchemaDefContext context)
        {
            using (new FieldConsumer(this, schemaInfo.Schema))
            {
                return base.VisitSchemaDef(context);
            }
        }
        public override object VisitEnumDef(GraphQLSchemaParser.EnumDefContext context)
        {
            var docComment = context.comment().LastOrDefault();
            var desc = docComment != null ? (string)VisitComment(docComment) : null;

            var result = base.VisitEnumDef(context);
            return result;
        }
        public override object VisitInputDef(GraphQLSchemaParser.InputDefContext context)
        {
            var docComment = context.comment().LastOrDefault();
            var desc = docComment != null ? (string)VisitComment(docComment) : null;

            var fields = new List<Field>();
            using (new FieldConsumer(this, fields))
            {
                var result = base.Visit(context.objectDef());
                schemaInfo.Inputs.Add(context.typeName.Text, new TypeInfo(fields, context.typeName.Text, desc, isInput:true));
                return result;
            }
        }
        public override object VisitTypeDef(GraphQLSchemaParser.TypeDefContext context)
        {
            var docComment = context.comment().LastOrDefault();
            var desc = docComment != null ? (string)VisitComment(docComment) : null;

            var fields = new List<Field>();
            using (new FieldConsumer(this, fields))
            {
                var result = base.Visit(context.objectDef());
                schemaInfo.Types.Add(context.typeName.Text, new TypeInfo(fields, context.typeName.Text, desc));
                return result;
            }
        }
        public override object VisitScalarDef(GraphQLSchemaParser.ScalarDefContext context)
        {
            var result = base.VisitScalarDef(context);
            return result;
        }
    }
}