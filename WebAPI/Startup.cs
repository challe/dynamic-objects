using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ObjectLibrary.ExtensionMethods;
using ObjectLibrary;
using ObjectLibrary.Models;
using ObjectLibrary.Services;

namespace WebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();            

            var deserializer = new YamlDeserializer();
            var settings = deserializer.DeserializeConfiguration<Settings>();

            settings.DynamicObjects.AddIdentityColumns();
            settings.DynamicObjects.CreateTypes();

            services.AddAllServices(settings);
            services.AddSingleton<HelloWorldQuery>();

            var entityTypes = CustomTypeBuilder.GetAllCustomTypes();

            foreach (var type in entityTypes)
            {
                // TODO: Implement
                //var myGenericObject = Activator.CreateInstance(typeof(DynamicObjectType<>).MakeGenericType(type));
                //services.AddSingleton(myGenericObject);
            }

            // TODO: Make generic (using foreach loop above)
            services.AddSingleton<CompanyType>();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new GraphQLSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            DynamicObjectService service = serviceProvider.GetRequiredService<DynamicObjectService>();

            dynamic obj = CustomTypeBuilder.CreateInstance("Company");
            obj.Name = "Sörens El";

            service.Create(obj);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
