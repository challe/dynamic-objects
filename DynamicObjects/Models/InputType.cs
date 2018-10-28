using System;
using System.Collections.Generic;

namespace DynamicObjects.Models
{
    public enum InputType
    {
        Text,
        Number,
        DateTime
    }

    internal static class InputTypeExtensions
    {
        public static Type ToType(this InputType inputType)
        {
            var mapper = new Dictionary<InputType, string>
            {
                { InputType.Text, "string" },
                { InputType.Number, "int" },
                { InputType.DateTime, "System.DateTime" }
            };

            return CustomTypeBuilder.GetType(mapper[inputType]);
        }
    }
}