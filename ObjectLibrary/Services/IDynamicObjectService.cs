using System.Collections.Generic;

namespace ObjectLibrary.Services
{
    public interface IDynamicObjectService
    {
        void Create<T>(T entity) where T : class;

        List<T> Find<T>() where T : class;

        T FindById<T>(int id) where T : class;
    }
}