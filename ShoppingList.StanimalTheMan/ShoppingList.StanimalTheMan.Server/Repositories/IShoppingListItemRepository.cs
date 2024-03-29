using ShoppingList.StanimalTheMan.Server.Models;

namespace ShoppingList.StanimalTheMan.Server.Repositories;

public interface IShoppingListItemRepository
{
	Task AddShoppingListItemAsync(ShoppingListItem shoppingListItem);
	Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id);
	Task<IEnumerable<ShoppingListItem>> GetAllShoppingListItemsAsync();
	Task UpdateShoppingListItemAsync(ShoppingListItem shoppingListItem);
	Task DeleteShoppingListItemAsync(ShoppingListItem shoppingListItem);
}
