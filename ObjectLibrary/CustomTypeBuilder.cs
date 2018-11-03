using GraphQL.SchemaGenerator.Attributes;
using ObjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ObjectLibrary
{
    public static class CustomTypeBuilder
    {
        public static List<Type> GetAllCustomTypes()
        {
            var assembly = CustomAssemblyBuilder.GetCustomAssembly(); 

            try
            {
                return assembly.GetTypes().ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null).ToList();
            }
        }

        public static Type GetType(string simpleTypeName)
        {
            if (SystemTypes.Contains(simpleTypeName))
            {
                string fullName = SystemTypes.GetFullNameFromTypeName(simpleTypeName);
                return Type.GetType(fullName);
            }
            else
            {
                var customTypes = GetAllCustomTypes();
                if (customTypes.Any(x => x.Name == simpleTypeName))
                {
                    return customTypes.Where(x => x.Name == simpleTypeName).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}