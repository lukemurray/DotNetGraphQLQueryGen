using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_gqlgen
{
    public class SchemaInfo
    {
        public Dictionary<string, string> typeMappings = new Dictionary<string, string> {
            {"String", "string"},
            {"ID", "string"},
            {"Int", "int?"},
            {"Float", "double?"},
            {"Boolean", "bool?"},
            {"String!", "string"},
            {"ID!", "string"},
            {"Int!", "int"},
            {"Float!", "double"},
            {"Boolean!", "bool"},
        };

        public SchemaInfo(Dictionary<string, string> typeMappings)
        {
            if (typeMappings != null)
            {
                foreach (var item in typeMappings)
                {
                    // overrides
                    this.typeMappings[item.Key] = item.Value;
                }
            }
            Schema = new List<Field>();
            Types = new Dictionary<string, TypeInfo>();
            Inputs = new Dictionary<string, TypeInfo>();
            Scalars = new List<string>();
        }

        public List<Field> Schema { get; }
        /// <summary>
        /// Return the query type info.
        /// </summary>
        public TypeInfo Query => Types[Schema.First(f => f.Name == "query").TypeName];
        /// <summary>
        /// Return the mutation type info.
        /// </summary>
        public TypeInfo Mutation
        {
            get
            {
                var typeName = Schema.First(f => f.Name == "mutation")?.TypeName;
                if (typeName != null)
                    return Types[typeName];
                return null;
            }
        }
        public Dictionary<string, TypeInfo> Types { get; }
        public Dictionary<string, TypeInfo> Inputs { get; }
        public List<string> Scalars { get; }

        internal bool HasDotNetType(string typeName)
        {
            return typeMappings.ContainsKey(typeName) || Types.ContainsKey(typeName) || Inputs.ContainsKey(typeName);
        }

        internal string GetDotNetType(string typeName)
        {
            if (typeMappings.ContainsKey(typeName))
                return typeMappings[typeName];
            if (Types.ContainsKey(typeName))
                return Types[typeName].Name;
            return Inputs[typeName].Name;
        }
    }

    public class TypeInfo
    {
        public TypeInfo(IEnumerable<Field> fields, string name, string description, bool isInput = false)
        {
            Fields = fields.ToList();
            Name = name;
            Description = description;
            IsInput = isInput;
        }

        public List<Field> Fields { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsInput { get; }
    }

    public class Field
    {
        private readonly SchemaInfo schemaInfo;

        public Field(SchemaInfo schemaInfo)
        {
            Args = new List<Arg>();
            this.schemaInfo = schemaInfo;
        }

        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsArray { get; set; }
        public List<Arg> Args { get; set; }
        public string Description { get; set; }

        public string DotNetName => Name[0].ToString().ToUpper() + string.Join("", Name.Skip(1));
        public string DotNetType
        {
            get
            {
                return IsArray ? $"List<{DotNetTypeSingle}>" : DotNetTypeSingle;
            }
        }
        public string DotNetTypeSingle
        {
            get
            {
                if (!schemaInfo.HasDotNetType(TypeName))
                {
                    throw new SchemaException($"Unknown dotnet type for schema type '{TypeName}'. Please provide a mapping for any custom scalar types defined in the schema");
                }
                return schemaInfo.GetDotNetType(TypeName);
            }
        }

        public bool ShouldBeProperty
        {
            get
            {
                return (Args.Count == 0 && !schemaInfo.Types.ContainsKey(TypeName) && !schemaInfo.Inputs.ContainsKey(TypeName)) || schemaInfo.Scalars.Contains(TypeName);
            }
        }

        public string ArgsOutput()
        {
            if (!Args.Any())
                return "";
            return string.Join(", ", Args.Select(a => $"{a.DotNetType} {a.Name}"));
        }

        public override string ToString()
        {
            return $"{Name}:{(IsArray ? '[' + TypeName + ']': TypeName)}";
        }
    }

    public class Arg : Field
    {
        public Arg(SchemaInfo schemaInfo) : base(schemaInfo)
        {
        }

        public bool Required { get; set; }
    }
}