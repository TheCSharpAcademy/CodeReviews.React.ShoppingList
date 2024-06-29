using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) :DbContext(options)
{
  public DbSet<Product> Products { get; set; }
}