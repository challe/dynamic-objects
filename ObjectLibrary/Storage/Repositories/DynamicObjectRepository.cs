using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace ObjectLibrary.Storage.Repositories
{
    public class DynamicObjectRepository : IDynamicObjectRepository
    {
        private readonly Context _context;

        public DynamicObjectRepository(Context context)
        {
            _context = context;
        }

        public T Create<T>(T entity) where T : class
        {
            SetDefaultValues(entity);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public List<T> Find<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public T FindById<T>(int id) where T : class
        {
            return _context.Set<T>().Where($"id == @0", id).FirstOrDefault();
        }

        private void SetDefaultValues<T>(T entity)
        {
            TrySetProperty(entity, "Created", DateTime.Now);
            TrySetProperty(entity, "CreatedBy", 1);
            TrySetProperty(entity, "Modified", DateTime.Now);
            TrySetProperty(entity, "ModifiedBy", 1);
        }

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }
    }
}