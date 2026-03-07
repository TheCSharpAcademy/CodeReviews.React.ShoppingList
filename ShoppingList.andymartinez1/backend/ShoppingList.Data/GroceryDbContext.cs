using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;

namespace ShoppingList.Data;

public class GroceryDbContext : DbContext
{
    public GroceryDbContext(DbContextOptions<GroceryDbContext> options)
        : base(options)
    {
    }

    public DbSet<GroceryItem> GroceryItems { get; set; }
}