using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Entities;

public class GroceryItem
{
    public int Id { get; set; }

    [StringLength(
        20,
        ErrorMessage = "{0} must be between {2} and {1} characters long.",
        MinimumLength = 3
    )]
    public string? Name { get; set; }

    public int Quantity { get; set; }

    public DateTime DateAdded { get; set; }

    public bool IsPurchased { get; set; }
}