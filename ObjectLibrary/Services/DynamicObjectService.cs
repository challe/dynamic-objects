using ObjectLibrary.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectLibrary.Services
{
    public class DynamicObjectService : IDynamicObjectService
    {
        private readonly IDynamicObjectRepository _dynamicObjectRepository;

        public DynamicObjectService(IDynamicObjectRepository dynamicObjectRepository)
        {
            _dynamicObjectRepository = dynamicObjectRepository;
        }

        public T Create<T>(T entity) where T : class
        {
            return _dynamicObjectRepository.Create(entity);
        }

        public T Update<T>(T entity) where T : class
        {
            return _dynamicObjectRepository.Update(entity);
        }

        public List<T> Find<T>() where T : class
        {
            var list = _dynamicObjectRepository.Find<T>();
            return list;
        }

        public T FindById<T>(int id) where T : class
        {
            var entity = _dynamicObjectRepository.FindById<T>(id);
            return entity;
        }
    }
}
