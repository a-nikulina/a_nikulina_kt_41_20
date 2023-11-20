using proekt.Interfaces.StudSessionInterface;
using static proekt.Interfaces.StudSessionInterface.IStudSessionInterface;

namespace proekt.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudSessionService, StudSessionService>();
            return services;
        }
    }
}
