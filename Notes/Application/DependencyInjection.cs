using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(GetConfiguredMappingConfig());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }

        private static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            IList<IRegister> registers = config.Scan(Assembly.GetExecutingAssembly());

            config.Apply(registers);

            return config;
        } 
    }
}
