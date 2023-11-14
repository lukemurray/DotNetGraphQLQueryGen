using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet_gqlgen
{
    public class SchemaInfo
    {
        public Dictionary<string, string> gqlToDotnetTypeMappings = new Dictionary<string, string> {
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
                    this.gqlToDotnetTypeMappings[item.Key] = item.Value;
                }
            }
            Schema = new List<Field>();
            Types = new Dictionary<string, TypeInfo>();
            Inputs = new Dictionary<string, TypeInfo>();
            Enums = new Dictionary<string, List<EnumInfo>>();
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
                var typeName = Schema.FirstOrDefault(f => f.Name == "mutation")?.TypeName;
                if (typeName != null)
                    return Types[typeName];
                return null;
            }
        }
        public Dictionary<string, TypeInfo> Types { get; }
        public Dictionary<string, TypeInfo> Inputs { get; }
        public Dictionary<string, List<EnumInfo>> Enums { get; set; }
        public List<string> Scalars { get; }

        internal bool HasDotNetType(string typeName)
        {
            return gqlToDotnetTypeMappings.ContainsKey(typeName) || Types.ContainsKey(typeName) || Inputs.ContainsKey(typeName) || Enums.ContainsKey(typeName);
        }

        internal string GetDotNetType(string typeName)
        {
            if (gqlToDotnetTypeMappings.ContainsKey(typeName))
                return gqlToDotnetTypeMappings[typeName];
            if (Types.ContainsKey(typeName))
                return Types[typeName].Name;
            if (Enums.ContainsKey(typeName))
                return typeName;
            return Inputs[typeName].Name;
        }
        internal bool IsEnum(string typeName)
        {
            return Enums.ContainsKey(typeName);
        }
    }

    public class EnumInfo
    {
        public EnumInfo(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string DotNetName => Name[0].ToString().ToUpper() + string.Join("", Name.Skip(1));
    }


    public class TypeInfo
    {
        public TypeInfo(IEnumerable<Field> fields, string name, string description, bool isInput = false, bool isInterface = false)
        {
            Fields = fields.ToList();
            Name = name;
            Description = description;
            IsInput = isInput;
            IsInterface = isInterface;
        }

        public List<Field> Fields { get; }
        public string Name { get; }
        public string QueryName => $"Query{Name}";
        public string Description { get; }
        public string DescriptionForComment(int indent = 8)
        {
            return string.Join("\n", Description.Split("\n").Select(l => l.Trim()).Where(l => l.Count() > 0).Select(l => $"/// {l}".PadLeft(indent + l.Length + 4))) + "\n";
        }

        public bool IsInput { get; }
        public bool IsInterface { get; }
    }

    public class Field
    {
        private readonly SchemaInfo schemaInfo;

        public Field(SchemaInfo schemaInfo)
        {
            Args = new List<Arg>();
            this.schemaInfo = schemaInfo;
        }
        public virtual string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsArray { get; set; }
        public bool IsScalar
        {
            get
            {
                return (!schemaInfo.Types.ContainsKey(TypeName) && !schemaInfo.Inputs.ContainsKey(TypeName)) || schemaInfo.Scalars.Contains(TypeName);
            }
        }
        public bool IsNonNullable { get; set; }
        public List<Arg> Args { get; set; }
        public string Description { get; set; }
        public string DescriptionForComment(int indent = 8)
        {
            return string.Join("\n", Description.Split("\n").Select(l => l.Trim()).Where(l => l.Count() > 0).Select(l => $"/// {l}".PadLeft(indent + l.Length + 4))) + "\n";
        }

        public string DotNetName => Name[0].ToString().ToUpper() + string.Join("", Name.Skip(1));
        public string DotNetType
        {
            get
            {
                var t = DotNetTypeSingle;
                if (IsNonNullable)
                    t = t.Trim('?');
                else if (!t.EndsWith('?') && schemaInfo.IsEnum(t))
                    t = t + "?";
                return IsArray ? $"List<{t}>" : t;
            }
        }
        public string DotNetTypeSingle
        {
            get
            {
                if (!schemaInfo.HasDotNetType(TypeName))
                {
                    if (schemaInfo.Scalars.Contains(TypeName))
                        return TypeName;
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

        /// <summary>
        /// Outputs the method signature with all arguments and a object selection argument if applicable
        /// </summary>
        /// <returns></returns>
        public string OutputMethodSignature(bool onlyRequiredArgs, bool withSelection)
        {
            // if we are not outputing withSelection and have only required args and return a
            // scalar, skip it otherwise we'll have duplicate method
            if (!withSelection && IsScalar && Args.All(a => a.Required))
                return null;

            var typeName = !withSelection ? DotNetTypeSingle : "TReturn";

            var sb = new StringBuilder("        public abstract ");
            sb.Append(IsScalar ? $"{DotNetType} " : IsArray ? $"List<{typeName}> " : $"{typeName} ");
            sb.Append(DotNetName).Append(IsScalar || !withSelection ? "(" : $"<{typeName}>(");
            var argsOut = ArgsOutput(onlyRequiredArgs);
            sb.Append(argsOut);
            if (withSelection && !IsScalar)
            {
                if (argsOut.Length > 0)
                    sb.Append(", ");
                sb.Append($"Expression<Func<Query{DotNetTypeSingle}, TReturn>> selection");
            }
            sb.Insert(0, BuildCommentDoc(withSelection));

            sb.AppendLine($");");

            return sb.ToString();
        }

        private string BuildCommentDoc(bool withSelection)
        {
            var sb = new StringBuilder();
            sb.AppendLine("        /// <summary>");
            if (Description != null)
                sb.Append($"{DescriptionForComment(8)}");
            if (!IsScalar && !withSelection)
            {
                if (Description != null)
                    sb.AppendLine($"        ///");
                sb.AppendLine($"        /// This shortcut will return a selection of all fields");
            }
            sb.AppendLine("        /// </summary>");
            if (!IsScalar && withSelection)
                sb.AppendLine(@"        /// <param name=""selection"">Projection of fields to select from the object</param>");
            sb.AppendLine($"        [GqlFieldName(\"{Name}\")]");
            return sb.ToString();
        }

        public string ArgsOutput(bool onlyRequiredArgs)
        {
            if (!Args.Any())
                return "";
            var result = string.Join(", ", Args.Where(a => !onlyRequiredArgs || a.Required).Select(a => $"{(a.Required ? a.DotNetType.Trim('?') : a.DotNetType)} {a.Name}"));
            return result;
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

        public override string Name
        {
            get => CSharpKeywords.EscapeIdentifier(base.Name);
            set => base.Name = value;
        }
    }
}