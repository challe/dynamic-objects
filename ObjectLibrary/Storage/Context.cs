using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectLibrary.Storage
{
    public class Context : DbContext
    {
        private readonly List<Type> _entityTypes;

        public Context(DbContextOptions options) : base(options)
        {
            _entityTypes = CustomTypeBuilder.GetAllCustomTypes();

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _entityTypes.ForEach(type => CreateEntity(modelBuilder, type));
        }

        private void CreateEntity(ModelBuilder modelBuilder, Type type)
        {
            var method = modelBuilder.GetType().GetMethods().Where(m =>
                m.Name == nameof(ModelBuilder.Entity) &&
                m.ContainsGenericParameters).First();
            method.MakeGenericMethod(type).Invoke(modelBuilder, null);

            foreach (var pi in (type).GetProperties())
            {
                if (pi.Name == "Id")
                    modelBuilder.Entity(type).HasKey(pi.Name);

                // If the property type is a custom one, ignore it and add instead an id
                if (_entityTypes.Contains(pi.PropertyType))
                {
                    modelBuilder.Entity(type).Property(typeof(int?), $"{pi.Name}Id");
                }
            }
        }
    }
}