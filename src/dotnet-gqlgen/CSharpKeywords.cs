using System.Collections.Generic;

namespace dotnet_gqlgen
{
    static class CSharpKeywords
    {
        /// <summary>
        /// List of C# reserved identifiers. 
        /// Note that contextual keyword can be used as var name
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/"/>
        /// </summary>
        private static readonly HashSet<string> reservedIdentifiers = new HashSet<string>
        {
            // reserved identifiers
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        /// <summary>
        /// Checks if value is a C# keyword
        /// </summary>
        public static bool IsReservedIdentifier(string value)
        {
            return reservedIdentifiers.Contains(value);
        }

        /// <summary>
        /// If identifier is a reserved one then will escape it by prefixing with a @
        /// </summary>
        public static string EscapeIdentifier(string identifier)
        {
            return IsReservedIdentifier(identifier) ? $"@{identifier}" : identifier;
        }
    }
}
