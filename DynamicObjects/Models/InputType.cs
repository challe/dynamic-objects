using System;
using System.Collections.Generic;

namespace DynamicObjects.Models
{
    public enum InputType
    {
        Text,
        Number
    }

    internal static class InputTypeExtensions
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