using Microsoft.EntityFrameworkCore;
using ShoppingList.StevieTV.Models;

namespace ShoppingList.StevieTV.Database;

public class ShoppingListContext : DbContext
{
    public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
    {
    }

    public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
}