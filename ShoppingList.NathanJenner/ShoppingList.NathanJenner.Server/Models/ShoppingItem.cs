namespace ShoppingList.NathanJenner.Server.Models;

public class ShoppingItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public bool IsPurchased { get; set; }
    public int SortOrder { get; set; }
}