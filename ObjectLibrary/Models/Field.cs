using System;
using YamlDotNet.Serialization;

namespace ObjectLibrary.Models
{
    public class Field
    {
        public Field()
        {
        }

        public Field(string name, InputType type, bool nullable = false)
        {
            Name = name;
            InputType = type;
            IsNullable = nullable;
        }

        public string Name { get; set; }

        public bool ExcludeFromGraph { get; set; }

        public bool IsNullable { get; set; }

        [YamlMember(Alias = "type")]
        public InputType InputType { get; set; }

        public Type Type
        {
            get { return InputType.ToType(IsNullable); }
        }
    }
}