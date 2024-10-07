using Microsoft.EntityFrameworkCore;
using ShoppingList.Server.Data;

namespace ShoppingList.Server.Installers;

/// <summary>
/// Registers dependencies and adds any required middleware.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("ShoppingList") ?? throw new InvalidOperationException("Connection string 'ShoppingList' not found");

        services.AddDbContext<ShoppingListDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(options =>
        {
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        return app;
    }

    public static WebApplication SetUpDatabase(this WebApplication app)
    {
        // Performs any database migrations and seeds the database.
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        services.SeedDatabase();

        return app;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ShoppingListDataContext>();
        context.Database.Migrate();

        return serviceProvider;
    }
}
