using AutoMapper;
using ShoppingList.Dtos;
using ShoppingList.Entities;
using ShoppingList.Exceptions;
using ShoppingList.Repositories;

namespace ShoppingList.Services;

public class ShoppingListService : IShoppingListService
{
    private readonly IShoppingListRepository _repository;
    private readonly IMapper _mapper;

    public ShoppingListService(
        IShoppingListRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ShoppingListItemDto>> GetItems()
    {
        var items = await _repository.GetItems();

        if (items.Count == 0)
        {
            throw new NotFoundException("there are no items in the database");
        }

        return _mapper.Map<List<ShoppingListItemDto>>(items);
    }

    public async Task<ShoppingListItemDto> GetItem(Guid id)
    {
        var item = await _repository.GetItem(id);

        if (item is null)
        {
            throw new NotFoundException($"item with id {id} not found");
        }

        return _mapper.Map<ShoppingListItemDto>(item);
    }

    public async Task<ShoppingListItemDto> AddItem(ShoppingListItemDto itemDto)
    {
        var item = _mapper.Map<ShoppingListItem>(itemDto);

        var created = await _repository.AddItem(item);
        await _repository.SaveChanges();

        return _mapper.Map<ShoppingListItemDto>(created);
    }

    public async Task DeleteItem(Guid id)
    {
        var item = await _repository.GetItem(id);

        if (item is null)
        {
            throw new NotFoundException($"item with id {id} not found");
        }

        _repository.DeleteItem(item);
        await _repository.SaveChanges();
    }

    public async Task MarkAsPicked(Guid id)
    {
        var item = await _repository.GetItem(id);

        if (item is null)
        {
            throw new NotFoundException($"item with id {id} not found");
        }

        item.IsPickedUp = !item.IsPickedUp;

        _repository.UpdateItem(item);
        await _repository.SaveChanges();
    }
}