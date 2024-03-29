using ShoppingList.StanimalTheMan.Server.Models;
using ShoppingList.StanimalTheMan.Server.Repositories;

namespace ShoppingList.StanimalTheMan.Server.Services;

public class ShoppingListItemService : IShoppingListItemService
{
	private readonly IShoppingListItemRepository _shoppingListItemRepository;

	public ShoppingListItemService(IShoppingListItemRepository shoppingListItemRepository)
	{
		_shoppingListItemRepository = shoppingListItemRepository;
	}
	public async Task AddShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		await _shoppingListItemRepository.AddShoppingListItemAsync(shoppingListItem);
	}

	public async Task DeleteShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		await _shoppingListItemRepository.DeleteShoppingListItemAsync(shoppingListItem);
	}

	public async Task<IEnumerable<ShoppingListItem>> GetAllShoppingListItemsAsync()
	{
		return await _shoppingListItemRepository.GetAllShoppingListItemsAsync();
	}

	public async Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id)
	{
		return await _shoppingListItemRepository.GetShoppingListItemByIdAsync(id);
	}

	public async Task UpdateShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		await _shoppingListItemRepository.UpdateShoppingListItemAsync(shoppingListItem);
	}
}
