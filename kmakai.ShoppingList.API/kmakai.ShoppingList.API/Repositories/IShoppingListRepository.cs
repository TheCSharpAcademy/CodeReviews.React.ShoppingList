using kmakai.ShoppingList.API.Models;

namespace kmakai.ShoppingList.API.Repositories;

public interface IShoppingListRepository
{
    public IEnumerable<ShoppingListItem> GetShoppingListItemsAsync();

    public void AddShoppingListItemAsync(ShoppingListItem item);

    public void UpdateShoppingListItemAsync(ShoppingListItem item);
}
