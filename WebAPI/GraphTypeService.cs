using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class GraphTypeService
    {
        public List<IGraphType> GetAllGraphTypes()
        {
            return new List<IGraphType>()
            {
                new CompanyType()
            };
        }
    }
}
