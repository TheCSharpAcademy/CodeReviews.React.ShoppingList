using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingList.Data;
using ShoppingList.Entities;
using ShoppingList.ServiceContracts;
using ShoppingList.ServiceContracts.DTO;

namespace ShoppingList.Services;

public class GroceryService : IGroceryService
{
    private readonly GroceryDbContext _context;
    private readonly ILogger<GroceryService> _logger;

    public GroceryService(GroceryDbContext context, ILogger<GroceryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroceryItemResponse> AddAsync(GroceryItemAddRequest? addRequest)
    {
        ArgumentNullException.ThrowIfNull(addRequest);

        var groceryItem = MapToGroceryItem(addRequest);

        await _context.GroceryItems.AddAsync(groceryItem);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToGroceryResponse(groceryItem);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToGroceryResponse(groceryItem);
        }

        _logger.LogInformation("Product with ID: {id} added.", groceryItem.Id);
        return MapToGroceryResponse(groceryItem);
    }

    public async Task<List<GroceryItemResponse>> GetAllAsync()
    {
        var groceryItems = await _context.GroceryItems.ToListAsync();

        return groceryItems.Select(MapToGroceryResponse).ToList();
    }

    public async Task<GroceryItemResponse?> GetByIdAsync(int id)
    {
        var groceryItem = await _context.GroceryItems.FirstOrDefaultAsync(gi => gi.Id == id);

        if (groceryItem is null)
            return null;

        _logger.LogInformation("Product with ID: {id} retrieved.", groceryItem.Id);
        return MapToGroceryResponse(groceryItem);
    }

    public async Task<GroceryItemResponse> UpdateAsync(GroceryItemUpdateRequest? updateRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRequest);

        var groceryItem = MapToGroceryItem(updateRequest);

        _context.GroceryItems.Update(groceryItem);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToGroceryResponse(groceryItem);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToGroceryResponse(groceryItem);
        }

        _logger.LogInformation("Product with ID: {id} updated.", groceryItem.Id);
        return MapToGroceryResponse(groceryItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var groceryItem = await _context.GroceryItems.FindAsync(id);

        if (groceryItem is null)
            return false;

        _context.GroceryItems.Remove(groceryItem);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return false;
        }

        _logger.LogInformation("Product with ID: {id} deleted.", groceryItem.Id);
        return true;
    }

    private static GroceryItem MapToGroceryItem(GroceryItemAddRequest request)
    {
        return new GroceryItem
        {
            Name = request.Name,
            Quantity = request.Quantity,
            DateAdded = request.DateAdded,
            IsPurchased = request.IsPurchased
        };
    }

    private static GroceryItem MapToGroceryItem(GroceryItemUpdateRequest request)
    {
        return new GroceryItem
        {
            Id = request.Id,
            Name = request.Name,
            Quantity = request.Quantity,
            DateAdded = request.DateAdded,
            IsPurchased = request.IsPurchased
        };
    }

    private static GroceryItemResponse MapToGroceryResponse(GroceryItem item)
    {
        return new GroceryItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            DateAdded = item.DateAdded,
            IsPurchased = item.IsPurchased
        };
    }
}