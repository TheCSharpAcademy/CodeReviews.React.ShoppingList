using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Server.Models;

/// <summary>
/// Represents a ShoppingListItem entity.
/// </summary>
public class ShoppingListItem
{
    #region Properties

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public bool IsPickedUp { get; set; }

    #endregion
}
