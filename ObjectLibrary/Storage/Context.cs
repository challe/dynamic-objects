using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectLibrary.Storage
{
    public class Context : DbContext
    {
        private readonly List<Type> _entityTypes;

        public Context(DbContextOptions options) : base(options)
        {
            _entityTypes = CustomTypeBuilder.GetAllCustomTypes();

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type type = _entityTypes.First();
            var typeOfContext = modelBuilder.GetType();
            var method = typeOfContext.GetMethods().Where(m =>
                m.Name == nameof(ModelBuilder.Entity) &&
                m.ContainsGenericParameters).First();
            var genericMethod = method.MakeGenericMethod(type);
            genericMethod.Invoke(modelBuilder, null);
        }
    }
}