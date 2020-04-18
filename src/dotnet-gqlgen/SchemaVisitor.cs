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
            var type = context.type.type?.Text;
            var arrayType = context.type.arrayType?.Text;
            addFieldsTo.Add(new Field(this.schemaInfo)
            {
                Name = name,
                TypeName = arrayType ?? type,
                IsArray = context.type.arrayType != null,
                Args = args,
                Description = desc,
                IsNonNullable = (context.type.elementTypeRequired ?? context.type.required) != null
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
                    var type = arg.dataType().type?.Text;
                    var arrayType = arg.dataType().arrayType?.Text;
                    args.Add(new Arg(this.schemaInfo)
                    {
                        Name = arg.NAME().GetText(),
                        TypeName = arrayType ?? type,
                        Required = (arg.dataType().arrayRequired ?? arg.dataType().required) != null,
                        IsArray = arrayType != null
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

            var fields = new List<Field>();
            using (new FieldConsumer(this, fields))
            {
                var result = base.VisitEnumDef(context);
                schemaInfo.Enums.Add(context.typeName.Text, fields.Select(f => f.Name).ToList());
                return result;
            }
        }
        public override object VisitEnumItem(GraphQLSchemaParser.EnumItemContext context)
        {
            this.addFieldsTo.Add(new Field(this.schemaInfo) {
                Name = context.name.Text,
                Args = new List<Arg>(),
            });
            return base.VisitEnumItem(context);
        }
        public override object VisitInputDef(GraphQLSchemaParser.InputDefContext context)
        {
            var docComment = context.comment().LastOrDefault();
            var desc = docComment != null ? (string)VisitComment(docComment) : null;

            var fields = new List<Field>();
            using (new FieldConsumer(this, fields))
            {
                var result = base.Visit(context.inputFields());
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
            schemaInfo.Scalars.Add(context.typeName.Text);
            return result;
        }
    }
}