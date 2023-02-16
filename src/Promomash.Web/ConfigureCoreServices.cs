using Promomash.Core.Interfaces;
using Promomash.Core.Services;
using Promomash.Infrastructure;

namespace Promomash.Web;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IProvinceService, ProvinceService>();

        return services;
    }
}
