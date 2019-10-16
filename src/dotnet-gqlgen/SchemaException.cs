using System;
using System.Runtime.Serialization;

namespace dotnet_gqlgen
{
    public class SchemaException : Exception
    {
        public SchemaException(string message) : base(message)
        {
        }

        public SchemaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}