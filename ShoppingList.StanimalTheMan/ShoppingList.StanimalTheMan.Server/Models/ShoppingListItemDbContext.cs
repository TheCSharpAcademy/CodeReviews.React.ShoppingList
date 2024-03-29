using Microsoft.EntityFrameworkCore;

namespace ShoppingList.StanimalTheMan.Server.Models;

public class ShoppingListItemDbContext : DbContext
{
	public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=(LocalDb)\\LocalDBDemo;Database=ShoppingListItems;Trusted_Connection=True;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ShoppingListItem>()
			.Property(x => x.IsPickedUp)
			.HasDefaultValue(false);

		base.OnModelCreating(modelBuilder);
	}
}
