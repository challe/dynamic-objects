using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace DynamicObjects.Models
{
    public class Field
    {
        public Field() { }

        public Field(string name, InputType type)
        {
            Name = name;
            InputType = type;
        }

        public string Name { get; set; }

        [YamlMember(Alias = "type")]
        public InputType InputType { get; set; }

        public Type Type
        {
            get { return InputType.ToType(); }
        }
    }
}
