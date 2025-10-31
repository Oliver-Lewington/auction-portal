using AuctionPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Extensions;

public static class DatabaseExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("DefaultConnection")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings_DefaultConnection");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<AuctionDbContext>(options => options.UseNpgsql(conn), ServiceLifetime.Scoped);
        services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
