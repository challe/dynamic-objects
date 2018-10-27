using DynamicObjects.Storage.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DynamicObjects.Storage
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, Models.Storage storage)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(storage.ConnectionString);
            });

            services.AddScoped<IDynamicObjectRepository, DynamicObjectRepository>();
            return services;
        }
    }
}
