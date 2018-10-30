using ObjectLibrary.Models;
using ObjectLibrary.Services;
using ObjectLibrary.Storage;
using ObjectLibrary.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ObjectLibrary.ExtensionMethods
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