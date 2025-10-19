using AuctionPortal.Components.Account;
using AuctionPortal.Components.Layout;
using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using AuctionPortal.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
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

        services.AddDbContext<AuctionDbContext>(options => options.UseNpgsql(conn), ServiceLifetime.Scoped);
        services.AddDatabaseDeveloperPageExceptionFilter();
    }

    public static void ConfigureAuthenticaiton(this IServiceCollection services)
    {
        // Blazor authentication state
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        // Use AddIdentity instead of AddIdentityCore for full UI support
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password requirements
            options.Password.RequiredLength = 12;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;

            // Sign-in settings
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<AuctionDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders()
        .AddDefaultUI();  // This enables the scaffolded Identity pages

        // Configure application cookie
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = ".AuctionPortal.Auth";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.ExpireTimeSpan = TimeSpan.FromDays(14);
            options.SlidingExpiration = true;

            // Important: Set correct paths for Identity pages
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        // Add Razor Pages support for Identity UI
        services.AddRazorPages();
    }

    public static void ConfigureHttp(this IServiceCollection services)
    {
        services.AddHttpClient();
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
            string redisConnectionString = configuration.GetConnectionString("Redis")
                ?? throw new NullReferenceException("Redis connection string is not configured.");
            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "AuctionPortal:";
        });

        services.AddSingleton<SessionCacheService>();
    }

    public static void ConfigureLogging(this ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Information);
        logging.AddConsole();
    }
}