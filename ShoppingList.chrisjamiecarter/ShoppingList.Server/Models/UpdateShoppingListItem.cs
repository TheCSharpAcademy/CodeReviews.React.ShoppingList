namespace ShoppingList.Server.Models;

/// <summary>
/// Represents a request to update a ShoppingListItem entity.
/// </summary>
public class UpdateShoppingListItem
{
    #region Properties

    public string? Name { get; set; }

    public bool IsPickedUp { get; set; }

    #endregion
}
