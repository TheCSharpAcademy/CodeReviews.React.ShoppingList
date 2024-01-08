using kmakai.ShoppingList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace kmakai.ShoppingList.API.Data;

public class ShoppingListContext: DbContext
{
    public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
    {
    }

    public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;
}
