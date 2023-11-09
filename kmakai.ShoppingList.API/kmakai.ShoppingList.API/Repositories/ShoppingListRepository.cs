using kmakai.ShoppingList.API.Data;
using kmakai.ShoppingList.API.Models;

namespace kmakai.ShoppingList.API.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly ShoppingListContext _context;

    public ShoppingListRepository(ShoppingListContext context)
    {
        _context = context;
    }

   public IEnumerable<ShoppingListItem> GetShoppingListItemsAsync()
    {
        return _context.ShoppingListItems;
    }

    public void AddShoppingListItemAsync(ShoppingListItem item)
    {
        _context.ShoppingListItems.Add(item);
        _context.SaveChanges();
    }

   public void UpdateShoppingListItemAsync(ShoppingListItem item)
    {
        _context.ShoppingListItems.Update(item);
        _context.SaveChanges();
    }

}
