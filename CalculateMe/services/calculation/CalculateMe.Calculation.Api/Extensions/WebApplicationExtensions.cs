using CalculateMe.Calculation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CalculateMe.Calculation.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseAppDefaults(this WebApplication app)
    {
        app.UseSwaggerUIForVersions();

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();
    }

    public static void UseSwaggerUIForVersions(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using CalculationDbContext dbContext = scope.ServiceProvider.GetRequiredService<CalculationDbContext>();

        dbContext.Database.EnsureCreated();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
