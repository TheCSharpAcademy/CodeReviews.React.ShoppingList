using System.ComponentModel.DataAnnotations;

namespace ShoppingList.ServiceContracts.DTO;

public class GroceryItemUpdateRequest
{
    public int Id { get; set; }

    [StringLength(
        30,
        ErrorMessage = "{0} must be between {2} and {1} characters long.",
        MinimumLength = 3
    )]
    public string? Name { get; set; }

    public int Quantity { get; set; }

    public DateTime DateAdded { get; set; }

    public bool IsPurchased { get; set; }
}