using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IProductsRepository
{
  Task<PaginatedProducts> GetProductsAsync(int page, int pageSize);
  Task<Product?> GetProductByIdAsync(int id);
  Task AddProductAsync(Product product);
  Task UpdateProductAsync(Product product);
  Task DeleteProductAsync(Product product);
}