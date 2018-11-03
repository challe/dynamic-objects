using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ObjectLibrary
{
    public static class PropertyUtilities
    {
        public static void TrySetProperty(object obj, string property, object value)
        {
            var prop = GetProperty(obj, property);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }

        public static object TryGetProperty(object obj, string property)
        {
            var prop = GetProperty(obj, property);
            if (prop != null && prop.CanRead)
                return prop.GetValue(obj);
            else
                return null;
        }

        public static bool HasProperty(object obj, string property)
        {
            return GetProperty(obj, property) != null;
        }

        private static PropertyInfo GetProperty(object obj, string property)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            return prop;
        }
    }
}
