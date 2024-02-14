using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListReact.Models
{
    public class ShoppingListContext: DbContext
    {
        public ShoppingListContext( DbContextOptions<ShoppingListContext> options ) : base(options) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasData(
                    new Item { Id = 1, Name = "Pasta" }
                );
        }
        public DbSet<Item> ShoppingListItems { get; set; }
    }
}
