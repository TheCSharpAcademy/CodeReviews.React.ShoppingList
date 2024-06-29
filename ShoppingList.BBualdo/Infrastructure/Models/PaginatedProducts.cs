namespace Infrastructure.Models;

public class PaginatedProducts
{
  public IEnumerable<Product>? Products { get; set; }
  public int TotalPages { get; set; }
}