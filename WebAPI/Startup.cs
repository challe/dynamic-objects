using GraphQL;
using GraphQL.SchemaGenerator;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ObjectLibrary;
using ObjectLibrary.ExtensionMethods;
using ObjectLibrary.Models;
using ObjectLibrary.Services;
using System;
using System.Collections.Generic;

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

            settings.DynamicObjects.AddDefaultFields();

            var objectGenerator = new ObjectGenerator();
            objectGenerator.CreateObjects(settings.DynamicObjects);

            services.AddAllServices(settings);

            var entityTypes = CustomTypeBuilder.GetAllCustomTypes();
            var schemaTypes = new List<Type>();
            foreach (var type in entityTypes)
            {
                IServiceProvider serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetRequiredService<DynamicObjectService>();
                var schemaType = Activator.CreateInstance(typeof(DynamicObjectType<>).MakeGenericType(type), new object[] { service }).GetType();
                schemaTypes.Add(schemaType);
                services.AddSingleton(schemaType);
            }

            IServiceProvider schemaServiceProvider = services.BuildServiceProvider();
            var schemaGenerator = new SchemaGenerator(schemaServiceProvider);
            var schema = schemaGenerator.CreateSchema(schemaTypes.ToArray());

            SchemaPrinter printer = new SchemaPrinter(schema);
            string wat = printer.Print();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ISchema>(schema);
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