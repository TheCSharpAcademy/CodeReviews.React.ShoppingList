namespace ShoppingList.Server.Models;

/// <summary>
/// Represents a request to create a ShoppingListItem entity.
/// </summary>
public class CreateShoppingListItem
{
    #region Properties

    public string? Name { get; set; }

    #endregion
}
