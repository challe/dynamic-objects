using GraphQL;
using GraphQL.Types;

namespace WebAPI
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<HelloWorldQuery>();
        }
    }
}