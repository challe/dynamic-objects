using System.Collections.Generic;

namespace DynamicObjects.Services
{
    public interface IDynamicObjectService
    {
        void Create<T>(T entity) where T : class;

        List<T> Find<T>() where T : class;
    }
}