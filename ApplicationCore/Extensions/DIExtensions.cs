using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationCore.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
