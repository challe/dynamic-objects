using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI
{
    public class DynamicObjectType<T> : ObjectGraphType<T> where T : class
    {
        public static Expression<Func<T, object>> GetLambda(string property)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression field = Expression.PropertyOrField(parameter, property);

            Expression<Func<T, object>> retval = Expression.Lambda<Func<T, object>>(Expression.Convert(field, typeof(object)), parameter);
            var test = retval.Compile();
            return retval;
        }

        public DynamicObjectType()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach(PropertyInfo propertyInfo in properties)
            {
                Expression<Func<T, object>> lambdaExpression = GetLambda(propertyInfo.Name);
                Field(lambdaExpression);
            }
        }
    }
}
