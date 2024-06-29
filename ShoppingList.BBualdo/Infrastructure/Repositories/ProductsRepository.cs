using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductsRepository(AppDbContext dbContext) : IProductsRepository
{
  private readonly AppDbContext _dbContext = dbContext;
  
  public async Task<PaginatedProducts> GetProductsAsync(int page, int pageSize)
  {
    var products = await _dbContext.Products.ToListAsync();
    var totalPages = (int)Math.Ceiling((double)products.Count / pageSize);
    var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize);

    return new PaginatedProducts
    {
      Products = paginatedProducts.OrderBy(p => p.Id),
      TotalPages = totalPages
    };
  }

  public async Task<Product?> GetProductByIdAsync(int id)
  {
    return await _dbContext.Products.FindAsync(id);
  }

  public async Task AddProductAsync(Product product)
  {
    await _dbContext.Products.AddAsync(product);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateProductAsync(Product product)
  {
    _dbContext.Entry(product).State = EntityState.Modified;
    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteProductAsync(Product product)
  {
    _dbContext.Products.Remove(product);
    await _dbContext.SaveChangesAsync();
  }
}