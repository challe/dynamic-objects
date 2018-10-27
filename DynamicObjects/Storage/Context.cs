using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DynamicObjects.Storage
{
    public class Context : DbContext
    {
        private readonly List<Type> _entityTypes;

        public Context(DbContextOptions options) : base(options)
        {
            _entityTypes = CustomTypeBuilder.GetAllCustomTypes();

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity");

            foreach (var type in _entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }
    }
}