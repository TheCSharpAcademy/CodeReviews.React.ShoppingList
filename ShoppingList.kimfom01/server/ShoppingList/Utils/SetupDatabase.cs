using ShoppingList.Data;

namespace ShoppingList.Utils;

public static class SetupDatabase
{
    public static void Setup(this WebApplication app, IWebHostEnvironment environment)
    {
        var scope = app.Services.CreateScope();

        var provider = scope.ServiceProvider;

        var context = provider.GetRequiredService<ShoppingListDbContext>();
        if (environment.IsDevelopment())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}