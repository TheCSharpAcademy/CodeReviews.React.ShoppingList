using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;

namespace ShoppingList.Data;

public class ShoppingListDbContext : DbContext
{
    public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

    public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ShoppingListItem>()
            .HasData(new List<ShoppingListItem>
            {
                new()
                {
                    Id = new Guid("afe8c295-69a5-4bcb-999d-f77cb2825b3d"),
                    CreatedAt = DateTime.Now,
                    IsPickedUp = false,
                    Item = "Milk"
                },
                new()
                {
                    Id = new Guid("e0f51482-b9e6-475e-b079-091bde7e99bf"),
                    CreatedAt = DateTime.Now,
                    IsPickedUp = false,
                    Item = "Sandwiches"
                },
                new()
                {
                    Id = new Guid("9f7fb0cf-4601-415b-9a12-1c00cb4054ea"),
                    CreatedAt = DateTime.Now,
                    IsPickedUp = false,
                    Item = "Eggs"
                },
                new()
                {
                    Id = new Guid("7ed75077-a8f9-4707-9d69-3d1f7c99ee7e"),
                    CreatedAt = DateTime.Now,
                    IsPickedUp = false,
                    Item = "Flour"
                }
            });
    }
}