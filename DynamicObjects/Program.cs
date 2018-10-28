﻿using DynamicObjects.ExtensionMethods;
using DynamicObjects.Models;
using DynamicObjects.Services;
using DynamicObjects.Storage;
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

            settings.DynamicObjects.AddIdentityColumns();
            settings.DynamicObjects.CreateTypes();

            IServiceCollection services = new ServiceCollection();
            services.AddAllServices(settings);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            DynamicObjectService service = serviceProvider.GetRequiredService<DynamicObjectService>();

            dynamic obj = CustomTypeBuilder.CreateInstance("Company");
            obj.Name = "Sörens El";

            service.Create(obj);

            Console.WriteLine(obj.Id);
            Console.WriteLine(obj.Name);

            Console.WriteLine("Hello World!");
        }
    }
}