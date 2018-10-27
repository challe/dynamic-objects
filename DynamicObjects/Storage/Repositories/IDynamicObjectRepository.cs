using System.Collections.Generic;

namespace DynamicObjects.Storage.Repositories
{
    public interface IDynamicObjectRepository
    {
        List<T> Find<T>() where T : class;
    }
}