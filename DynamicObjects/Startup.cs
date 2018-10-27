using DynamicObjects.Models;
using DynamicObjects.Storage;
using Microsoft.Extensions.DependencyInjection;

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