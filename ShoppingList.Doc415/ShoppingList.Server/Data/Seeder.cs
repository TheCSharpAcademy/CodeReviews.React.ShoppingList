using ShoppingList.Server.Models;

namespace ShoppingList.Server.Data;

public class Seeder
{
    public AppDbContext _context;

    public Seeder(AppDbContext context) { _context = context; }

    public async Task SeedDb()
    {
        var items = new List<ShopItem> { new ShopItem() { Name = "Carrot" }, new ShopItem() { Name = "Milk" }, new ShopItem() { Name = "Chocolatey" } };
        await _context.ShopItems.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }
}
