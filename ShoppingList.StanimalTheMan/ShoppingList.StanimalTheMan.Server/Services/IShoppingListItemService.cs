using ShoppingList.StanimalTheMan.Server.Models;

namespace ShoppingList.StanimalTheMan.Server.Services;

public interface IShoppingListItemService
{
	Task AddShoppingListItemAsync(ShoppingListItem shoppingListItem);
	Task DeleteShoppingListItemAsync(ShoppingListItem shoppingListItem);
	Task<IEnumerable<ShoppingListItem>> GetAllShoppingListItemsAsync();
	Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id);
	Task UpdateShoppingListItemAsync(ShoppingListItem shoppingListItem);
}
