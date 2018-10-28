using DynamicObjects.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicObjects.Services
{
    public class DynamicObjectService : IDynamicObjectService
    {
        private readonly IDynamicObjectRepository _dynamicObjectRepository;

        public DynamicObjectService(IDynamicObjectRepository dynamicObjectRepository)
        {
            _dynamicObjectRepository = dynamicObjectRepository;
        }

        public void Create<T>(T entity) where T : class
        {
            _dynamicObjectRepository.Create(entity);
        }

        public List<T> Find<T>() where T : class
        {
            var list = _dynamicObjectRepository.Find<T>();
            return list;
        }
    }
}
