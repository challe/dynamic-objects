using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ObjectLibrary.Models
{
    public class Field
    {
        public Field()
        {
        }

        public Field(string name, string type, bool nullable = false)
        {
            Name = name;
            InputType = type;
            IsNullable = nullable;
        }

        public string Name { get; set; }

        public bool ExcludeFromGraph { get; set; }

        public bool IsNullable { get; set; }

        [YamlMember(Alias = "type")]
        public string InputType { get; set; }

        public Type Type
        {
            get { return GetType(InputType, IsNullable); }
        }

        private Type GetType(string inputType, bool isNullable)
        {
            var mapper = new Dictionary<string, string>
            {
                { "Text", "string" },
                { "Number", "int" },
                { "DateTime", "System.DateTime" }
            };

            string name = String.Empty;
            if (mapper.ContainsKey(inputType))
            {
                name = mapper[inputType];
            }
            else
            {
                name = inputType;
            }

            var type = CustomTypeBuilder.GetType(name);

            if (isNullable)
                type = typeof(Nullable<>).MakeGenericType(type);

            return type;
        }
    }
}