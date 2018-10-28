﻿using System.Collections.Generic;

namespace DynamicObjects.Storage.Repositories
{
    public interface IDynamicObjectRepository
    {
        void Create<T>(T entity) where T : class;

        List<T> Find<T>() where T : class;
    }
}