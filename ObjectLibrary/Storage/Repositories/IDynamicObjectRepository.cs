using System.Collections.Generic;

namespace ObjectLibrary.Storage.Repositories
{
    public interface IDynamicObjectRepository
    {
        T Create<T>(T entity) where T : class;

        List<T> Find<T>() where T : class;

        T FindById<T>(int id) where T : class;
    }
}