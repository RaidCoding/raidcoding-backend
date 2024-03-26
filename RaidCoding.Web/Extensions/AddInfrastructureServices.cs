using Microsoft.EntityFrameworkCore;
using RaidCoding.Data;

namespace RaidCoding.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<RcDbContext>(o =>
            o.UseNpgsql(config["PostgresConnectionString"]));
    }
}