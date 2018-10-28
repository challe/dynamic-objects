using Microsoft.CSharp;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicObjects
{
    internal static class SystemTypes
    {
        private static readonly Dictionary<string, string> _types = GetAvailableSystemTypes();

        public static string GetFullNameFromTypeName(string typeName)
        {
            return _types[typeName];
        }

        private static Dictionary<string, string> GetAvailableSystemTypes()
        {
            var types = new Dictionary<string, string>();
            // Resolve reference to mscorlib.
            // int is an arbitrarily chosen type in mscorlib
            var mscorlib = Assembly.GetAssembly(typeof(int));

            using (var provider = new CSharpCodeProvider())
            {
                foreach (var type in mscorlib.DefinedTypes)
                {
                    if (string.Equals(type.Namespace, "System"))
                    {
                        var typeRef = new CodeTypeReference(type);
                        var csTypeName = provider.GetTypeOutput(typeRef);

                        // Ignore qualified types.
                        if (csTypeName.IndexOf('.') == -1 || csTypeName == "System.DateTime")
                        {
                            types.Add(csTypeName, type.FullName);
                        }
                    }
                }
            }

            return types;
        }

        internal static bool Contains(string simpleTypeName)
        {
            return _types.Any(x => x.Key == simpleTypeName);
        }
    }
}