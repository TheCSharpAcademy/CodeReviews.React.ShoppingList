using Microsoft.EntityFrameworkCore;
using ShoppingList.NathanJenner.Server.Models;

namespace ShoppingList.NathanJenner.Server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ShoppingItem> ShoppingItems { get; set; } = null!;
}