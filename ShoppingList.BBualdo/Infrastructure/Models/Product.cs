namespace Infrastructure.Models;

public class Product
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public bool IsPickedUp { get; set; }
}