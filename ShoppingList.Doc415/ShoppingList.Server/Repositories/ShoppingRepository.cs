using Microsoft.EntityFrameworkCore;
using ShoppingList.Server.Data;
using ShoppingList.Server.Models;

namespace ShoppingList.Server.Repositories;

public class ShoppingRepository : IShoppingRepository
{
    public AppDbContext _context;
    public ShoppingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShopItem>> GetItems()
    {
        try
        {
            var items = await _context.ShopItems.ToListAsync();
            return items;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<ShopItem> GetItemById(int id)
    {
        try
        {
            return await _context.ShopItems.FindAsync(id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task DeleteItem(int id)
    {
        var toRemove = _context.ShopItems.Find(id);
        _context.ShopItems.Remove(toRemove);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItem(ShopItem item)
    {
        try
        {
            _context.ShopItems.Update(item);
            _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task AddItem(ShopItem item)
    {
        try
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
