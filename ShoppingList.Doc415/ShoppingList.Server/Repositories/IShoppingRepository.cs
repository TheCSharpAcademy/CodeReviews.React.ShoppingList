using ShoppingList.Server.Models;

namespace ShoppingList.Server.Repositories;

public interface IShoppingRepository
{
    Task<List<ShopItem>> GetItems();
    Task<ShopItem> GetItemById(int id);
    Task DeleteItem(int id);
    Task UpdateItem(ShopItem item);
    Task<int> AddItem(ShopItem item);

}
