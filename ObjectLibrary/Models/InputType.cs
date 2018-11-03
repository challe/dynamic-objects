using System;
using System.Collections.Generic;

namespace ObjectLibrary.Models
{
    public enum InputType
    {
        Text,
        Number,
        DateTime
    }

    internal static class InputTypeExtensions
    {
        public static Type ToType(this InputType inputType, bool isNullable)
        {
            var mapper = new Dictionary<InputType, string>
            {
                { InputType.Text, "string" },
                { InputType.Number, "int" },
                { InputType.DateTime, "System.DateTime" }
            };

            var type = CustomTypeBuilder.GetType(mapper[inputType]);

            if (isNullable)
                type = typeof(Nullable<>).MakeGenericType(type);

            return type;
        }
    }
}