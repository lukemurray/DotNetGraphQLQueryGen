using System;

namespace DotNetGqlClient
{
    public class GqlFieldNameAttribute : Attribute
    {
        public string Name { get; }

        public GqlFieldNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}