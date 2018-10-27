using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicObjects.Models
{
    public enum InputType
    {
        Text,
        Number
    }

    static class InputTypeExtensions
    {
        public static Type ToType(this InputType inputType)
        {
            var mapper = new Dictionary<InputType, string>
            {
                { InputType.Text, "string" },
                { InputType.Number, "int" }
            };

            return CustomTypeBuilder.GetType(mapper[inputType]);
        }
    }
}
