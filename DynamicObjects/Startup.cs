using DynamicObjects.Models;
using DynamicObjects.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DynamicObjects
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, Settings settings)
        {
            services.AddStorage(settings.Storage);
        }
    }
}
