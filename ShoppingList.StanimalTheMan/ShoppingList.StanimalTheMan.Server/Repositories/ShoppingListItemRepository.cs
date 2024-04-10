using Microsoft.EntityFrameworkCore;
using ShoppingList.StanimalTheMan.Server.Models;

namespace ShoppingList.StanimalTheMan.Server.Repositories;

public class ShoppingListItemRepository : IShoppingListItemRepository
{
	private readonly ShoppingListItemDbContext _context;

	public ShoppingListItemRepository(ShoppingListItemDbContext context)
	{
		_context = context;
	}

	public async Task AddShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		await _context.ShoppingListItems.AddAsync(shoppingListItem);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		_context.ShoppingListItems.Remove(shoppingListItem);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<ShoppingListItem>> GetAllShoppingListItemsAsync()
	{
		return await _context.ShoppingListItems.ToListAsync();
	}

	public async Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id)
	{
		return await _context.ShoppingListItems.FindAsync(id);
	}

	public async Task UpdateShoppingListItemAsync(ShoppingListItem shoppingListItem)
	{
		_context.Entry(shoppingListItem).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}
}
