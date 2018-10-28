using System.Collections.Generic;
using System.Linq;

namespace DynamicObjects.Storage.Repositories
{
    public class DynamicObjectRepository : IDynamicObjectRepository
    {
        private readonly Context _context;

        public DynamicObjectRepository(Context context)
        {
            _context = context;
        }

        public void Create<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public List<T> Find<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
    }
}