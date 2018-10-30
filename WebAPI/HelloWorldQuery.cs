using GraphQL.Types;
using ObjectLibrary;
using ObjectLibrary.Services;
using ObjectLibrary.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebAPI
{
    public class HelloWorldQuery : ObjectGraphType
    {
        private readonly IDynamicObjectRepository _dynamicObjectRepository;

        public HelloWorldQuery(IDynamicObjectRepository dynamicObjectRepository)
        {
            _dynamicObjectRepository = dynamicObjectRepository;

            // TODO: Create interface and add using DI
            GraphTypeService graphTypeService = new GraphTypeService();

            var types = graphTypeService.GetAllGraphTypes();

            Field<StringGraphType>(
                name: "hello",
                resolve: context => "world"
            );
            
            foreach(IGraphType type in types) {
                // Makes sure the IGraphType (ie CompanyType) is used as Company
                string customTypeName = type.GetType().Name.Replace("Type", "");
                Type customType = CustomTypeBuilder.GetType(customTypeName);

                Field<CompanyType>(
                    name: customTypeName,
                    arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                    resolve: context =>
                    {
                        var id = context.GetArgument<int>("id");

                        var method = _dynamicObjectRepository.GetType().GetMethod("FindById");
                        var genericMethod = method.MakeGenericMethod(customType);
                        var result = genericMethod.Invoke(_dynamicObjectRepository, new object[] { id });
                        return result;
                    }
                );
                /*
                var method = typeof(ComplexGraphType<>).GetMethods().Where(m =>
                    m.Name == "Field" && !m.ContainsGenericParameters).First();
                var genericMethod = method.MakeGenericMethod(type);

                // ResolveFieldContext<TSourceType>
                var resolveFieldContext = typeof(ResolveFieldContext<>);
                var resolveFieldContextSourceType = resolveFieldContext.MakeGenericType(type);
                //object resolveFieldContextSourceType = Activator.CreateInstance(makeme);

                //Func<ResolveFieldContext<>, object>
                    var test = Expression.Lambda<Func<ResolveFieldContext, object>>
                var wat = Expression.Lambda<Func<TEntity, TCriteria, bool>>
                var resolve = new Func<resolveFieldContextSourceType, object>("wat");

                genericMethod.Invoke(this, new object[] {
                    "randomPlayer",
                    null,
                    null,
                    resolve,
                    string deprecationReason = null
                    , null, null, context => "":, _language, true, false });
                Func<ResolveFieldContext<TSourceType>, object>
                string name, string description = null, QueryArguments arguments = null, Func<ResolveFieldContext<TSourceType>, object> resolve = null, string deprecationReason = null

                Field<string>(
                    "randomPlayer",
                    resolve: context => "");

                */
            }
        }
    }
}