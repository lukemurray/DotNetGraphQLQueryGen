using System;
using System.Collections.Generic;

namespace dotnet_gqlgen
{
    internal class FieldConsumer : IDisposable
    {
        private readonly SchemaVisitor schemaVisitor;
        private List<Field> schema;

        public FieldConsumer(SchemaVisitor schemaVisitor, List<Field> schema)
        {
            this.schemaVisitor = schemaVisitor;
            this.schema = schema;
            schemaVisitor.SetFieldConsumer(schema);
        }

        public void Dispose()
        {
            schemaVisitor.SetFieldConsumer(null);
        }
    }
}