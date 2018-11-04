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
            PropertyUtilities.TrySetProperty(entity, "Created", DateTime.Now);
            PropertyUtilities.TrySetProperty(entity, "CreatedBy", 1);
            PropertyUtilities.TrySetProperty(entity, "Modified", DateTime.Now);
            PropertyUtilities.TrySetProperty(entity, "ModifiedBy", 1);

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

        public T Update<T>(T entity) where T : class
        {
            var fieldsToIgnore = new string[] { "Id", "Created", "CreatedBy", "Modified", "ModifiedBy" };
            int id = (int)PropertyUtilities.TryGetProperty(entity, "Id");
            T found = FindById<T>(id);

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (!fieldsToIgnore.Contains(property.Name))
                {
                    PropertyUtilities.TrySetProperty(found, property.Name, property.GetValue(entity));
                }
            }

            PropertyUtilities.TrySetProperty(found, "Modified", DateTime.Now);
            PropertyUtilities.TrySetProperty(found, "ModifiedBy", 1);

            _context.SaveChanges();

            return found;
        }
    }
}