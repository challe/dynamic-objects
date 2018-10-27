using DynamicObjects.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var deserializer = new YamlDeserializer();
            var settings = deserializer.DeserializeConfiguration<Settings>();

            settings.DynamicObjects.ForEach(type => CustomTypeBuilder.CreateType(type));

            // Types to store in DB
            List<Type> types = CustomTypeBuilder.GetAllCustomTypes();

            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services, settings);

            dynamic obj = CustomTypeBuilder.CreateInstance("Company");

            obj.Id = 123456;
            obj.Name = "Sörens El";

            Console.WriteLine(obj.Id);
            Console.WriteLine(obj.Name);

            Console.WriteLine("Hello World!");
        }
    }
}