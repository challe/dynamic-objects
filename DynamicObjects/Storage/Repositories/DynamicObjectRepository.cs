using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicObjects.Storage.Repositories
{
    public class DynamicObjectRepository : IDynamicObjectRepository
    {
        private readonly Context _context;

        public DynamicObjectRepository(Context context)
        {
            _context = context;
        }

        public List<T> Find<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
    }
}
