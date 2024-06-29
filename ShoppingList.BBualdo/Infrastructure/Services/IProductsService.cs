using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IProductsService
{
  Task<PaginatedProducts> GetAsync(int page, int pageSize);
  Task AddAsync(Product product);
  Task UpdateAsync(Product product);
  Task DeleteAsync(int id);
}