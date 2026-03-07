using ShoppingList.ServiceContracts.DTO;

namespace ShoppingList.ServiceContracts;

public interface IGroceryService
{
    public Task<GroceryItemResponse> AddAsync(GroceryItemAddRequest? addRequest);

    public Task<List<GroceryItemResponse>> GetAllAsync();

    public Task<GroceryItemResponse?> GetByIdAsync(int id);

    public Task<GroceryItemResponse> UpdateAsync(GroceryItemUpdateRequest? updateRequest);

    public Task<bool> DeleteAsync(int id);
}
