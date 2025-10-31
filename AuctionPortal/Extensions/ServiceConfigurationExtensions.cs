using AuctionPortal.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using StackExchange.Redis;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace AuctionPortal.Extensions;

public static class ServiceConfigurationExtensions
{
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
        services.AddScoped<IProductService, ProductService>();
    }

    public static void ConfigureFormOptions(this IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB upload limit
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

        // Load and protect keys with certificate (if available)
        if (File.Exists(certPath))
        {
            var cert = X509CertificateLoader.LoadPkcs12FromFile(certPath, certPassword);
            dataProtectionBuilder.ProtectKeysWithCertificate(cert);
        }
    }

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            string redisConnectionString = config.GetConnectionString("Redis")
                ?? throw new NullReferenceException("Redis connection string is not configured.");
            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.GetConnectionString("Redis");
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
