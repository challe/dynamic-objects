﻿using GraphQL.SchemaGenerator.Attributes;
using GraphQL.Types;
using ObjectLibrary.Services;
using ObjectLibrary.Storage;
using ObjectLibrary.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI
{
    public class DynamicObjectType<T> : ObjectGraphType<T> where T : class
    {
        private readonly IDynamicObjectService _dynamicObjectService;

        public DynamicObjectType(IDynamicObjectService dynamicObjectService)
        {
            _dynamicObjectService = dynamicObjectService;
        }

        [GraphRoute]
        public T DynamicObject(int id)
        {
            var method = _dynamicObjectService.GetType().GetMethod("FindById");
            var genericMethod = method.MakeGenericMethod(typeof(T));
            var result = genericMethod.Invoke(_dynamicObjectService, new object[] { id });

            return (T)result;
        }
    }
}
