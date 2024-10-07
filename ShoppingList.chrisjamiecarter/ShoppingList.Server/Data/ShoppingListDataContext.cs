using Microsoft.EntityFrameworkCore;
using ShoppingList.Server.Models;

namespace ShoppingList.Server.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the ShoppingList data store.
/// </summary>
public class ShoppingListDataContext : DbContext
{
    #region Constructors

    public ShoppingListDataContext(DbContextOptions<ShoppingListDataContext> options) : base(options) { }

    #endregion
    #region Properties

    public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = default!;

    #endregion
}
