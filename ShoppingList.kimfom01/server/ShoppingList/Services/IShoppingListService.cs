using ShoppingList.Dtos;

namespace ShoppingList.Services;

public interface IShoppingListService
{
    Task<List<ShoppingListItemDto>> GetItems();
    Task<ShoppingListItemDto> GetItem(Guid id);
    Task<ShoppingListItemDto> AddItem(ShoppingListItemDto itemDto);
    Task DeleteItem(Guid id);
    Task MarkAsPicked(Guid id);
}