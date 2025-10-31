using AuctionPortal.Data;
using AuctionPortal.Data.Seeding;
using AuctionPortal.Extensions;
using AuctionPortal.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add Razor / Interactive Server Components
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

// Configure Services
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAutoMapper(cfg => { }, typeof(AuctionMappingProfile));
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureHttp();
builder.Services.ConfigureMudBlazor();
builder.Services.ConfigureAuctionServices();
builder.Services.ConfigureFormOptions();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureDataProtection("/app/keys", "/app/certs/dataprotection.pfx", "certPassword");
builder.Services.AddHttpContextAccessor();

// Configure Logging
builder.Logging.ConfigureLogging();

// Build app
var app = builder.Build();

// Apply Migrations
app.ApplyDatabaseMigrations<AuctionDbContext>();

await AdminUserSeeder.SeedAdminAsync(app.Services);

// Configure HTTP pipeline
app.ConfigurePipeline();

app.Run();
