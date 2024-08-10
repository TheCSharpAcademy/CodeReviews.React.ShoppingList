using Microsoft.EntityFrameworkCore;
using ShoppingList.Server.Models;

namespace ShoppingList.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ShopItem> ShopItems { get; set; }
}
