using DynamicObjects.Models;
using DynamicObjects.Services;
using DynamicObjects.Storage;
using DynamicObjects.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicObjects.ExtensionMethods
{
    public static class IServiceCollectionExtension
    {   
        public static IServiceCollection AddAllServices(this IServiceCollection services, Settings settings)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(settings.Storage.ConnectionString);
            });

            services.AddTransient<IDynamicObjectRepository, DynamicObjectRepository>();
            services.AddTransient<IDynamicObjectService, DynamicObjectService>();
            services.AddTransient<DynamicObjectService>();
            return services;
        }
    }
}