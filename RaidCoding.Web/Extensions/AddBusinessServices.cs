using RaidCoding.Logic.Services;

namespace RaidCoding.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddBusinessServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IAuthService, AuthService>();
    }
}