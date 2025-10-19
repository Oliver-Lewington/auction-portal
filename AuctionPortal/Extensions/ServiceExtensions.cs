using AuctionPortal.Components.Layout;
using AuctionPortal.Data;
using AuctionPortal.Services;

//using AuctionPortal.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;

namespace AuctionPortal.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("DefaultConnection")
                   ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<AuctionDbContext>(options =>
            options.UseNpgsql(conn), ServiceLifetime.Scoped);
    }

    public static void ConfigureHttp(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddAntiforgery();
    }

    public static void ConfigureMudBlazor(this IServiceCollection services)
    {
        services.AddMudServices();
    }

    public static void ConfigureAuctionServices(this IServiceCollection services)
    {
        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IProductService, ProductService>();;
    }

    public static void ConfigureFormOptions(this IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
        });
    }

    public static void ConfigureDataProtection(this IServiceCollection services,
                                               string keysFolder,
                                               string certPath,
                                               string certPassword)
    {
        var dataProtectionBuilder = services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
            .SetApplicationName("AuctionPortal");

        if (File.Exists(certPath))
        {
            // Load certificate safely using the new API
            var cert = X509CertificateLoader.LoadPkcs12FromFile(certPath, certPassword);

            dataProtectionBuilder.ProtectKeysWithCertificate(cert);
        }
    }

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            string redisConnectionString = configuration.GetConnectionString("Redis") ?? throw new NullReferenceException("Redis connection string is not configured.");

            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "AuctionPortal:";
        });

        services.AddSingleton<SessionCacheService>();
    }

    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.Name = ".AuctionPortal.Session";
            options.IdleTimeout = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });
    }

    public static void ConfigureLogging(this ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Information);
        logging.AddConsole();
    }
}
