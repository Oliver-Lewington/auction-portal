using AuctionPortal.Components;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyDatabaseMigrations<TContext>(this WebApplication app) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();
         db.Database.Migrate();
    }

    public static void ConfigurePipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
        else
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();
        }

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();
    }
}
