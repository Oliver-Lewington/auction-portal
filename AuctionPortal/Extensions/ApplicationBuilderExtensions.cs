using AuctionPortal.Components;
using AuctionPortal.Data;
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
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();         // Developer-friendly error page
            app.UseMigrationsEndPoint();            // EF migration endpoint for dev
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();                           // Enforce HTTPS in production
            app.UseHttpsRedirection();               // Redirect HTTP → HTTPS in production
        }

        app.UseStaticFiles();

        // Anti-forgery protection (applies to forms, APIs, etc.)
        app.UseAntiforgery();

        // Razor components
        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        // Identity / Account endpoints
        app.MapAdditionalIdentityEndpoints();
    }
}
