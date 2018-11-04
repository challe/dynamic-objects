using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ObjectLibrary
{
    public static class CustomAssemblyBuilder
    {
        private static readonly string _customAssemblyName = "DynamicObjectsCustomAssembly";

        public static string CustomAssemblyName()
        {
            return _customAssemblyName;
        }

        public static AssemblyBuilder CreateAssemblyBuilder()
        {
            var assemblyName = new AssemblyName(_customAssemblyName);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            return assemblyBuilder;
        }

        public static Assembly GetCustomAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == CustomAssemblyName());
        }

        internal static ModuleBuilder CreateModuleBuilder()
        {
            return CreateAssemblyBuilder().DefineDynamicModule("MainModule");
        }
    }
}