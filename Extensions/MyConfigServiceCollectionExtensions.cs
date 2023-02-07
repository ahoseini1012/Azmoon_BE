
namespace Agricaltech;
public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzmoonetOptions>(configuration.GetSection(AzmoonetOptions.Azmoonet));
        return services;
    }
}