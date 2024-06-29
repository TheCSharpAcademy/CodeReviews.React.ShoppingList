using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ProductsService(IProductsRepository productsRepository) : IProductsService
{
  private readonly IProductsRepository _productsRepository = productsRepository;
  
  public async Task<PaginatedProducts> GetAsync(int page, int pageSize)
  {
    return await _productsRepository.GetProductsAsync(page, pageSize);
  }

  public async Task AddAsync(Product product)
  {
    await _productsRepository.AddProductAsync(product);
  }

  public async Task UpdateAsync(Product product)
  {
    await _productsRepository.UpdateProductAsync(product);
  }

  public async Task DeleteAsync(int id)
  {
    var product = await _productsRepository.GetProductByIdAsync(id);
    if (product is null) return;
    await _productsRepository.DeleteProductAsync(product);
  }
}