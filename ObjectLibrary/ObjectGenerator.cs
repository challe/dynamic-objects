﻿using GraphQL.SchemaGenerator.Attributes;
using ObjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using DynamicObject = ObjectLibrary.Models.DynamicObject;

namespace ObjectLibrary
{
    public class ObjectGenerator
    {
        private static ModuleBuilder _moduleBuilder;

        public ObjectGenerator()
        {
            _moduleBuilder = CustomAssemblyBuilder.CreateModuleBuilder();
        }

        public void CreateObjects(List<DynamicObject> list)
        {
            foreach (DynamicObject dynamicObject in list)
            {
                CreateType(dynamicObject);
            }
        }

        private void CreateType(DynamicObject dynamicObject)
        {
            var myTypeInfo = CompileResultTypeInfo(dynamicObject);
            var myType = myTypeInfo.AsType();
        }

        public static TypeInfo CompileResultTypeInfo(DynamicObject dynamicObject)
        {
            TypeBuilder tb = GetTypeBuilder(dynamicObject.Name);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (Field field in dynamicObject.Fields)
            {
                CreateProperty(tb, field);
            }

            TypeInfo objectTypeInfo = tb.CreateTypeInfo();
            return objectTypeInfo;
        }

        private static TypeBuilder GetTypeBuilder(string typeSignature)
        {
            TypeBuilder tb = _moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, Field field)
        {
            string propertyName = field.Name;
            Type propertyType = field.Type;

            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            // Custom attribute
            if (field.ExcludeFromGraph)
            {
                var attrCtorParams = new Type[] { typeof(Type), typeof(bool) };
                var attrCtorInfo = typeof(GraphTypeAttribute).GetConstructor(attrCtorParams);
                var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { null, true });
                propertyBuilder.SetCustomAttribute(attrBuilder);
            }
        }
    }
}