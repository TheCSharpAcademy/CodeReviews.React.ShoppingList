using kmakai.ShoppingList.API.Models;
using kmakai.ShoppingList.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace kmakai.ShoppingList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingListController : ControllerBase
{

    private readonly IShoppingListRepository _repository;

    public ShoppingListController(IShoppingListRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<ShoppingListItem> Get()
    {
        return _repository.GetShoppingListItemsAsync();
    }

    [HttpPost]
    public void Post([FromBody] ShoppingListItem item)
    {
        if (item != null)
            _repository.AddShoppingListItemAsync(item);
    }


    [HttpPut]
    public void Put([FromBody] ShoppingListItem item)
    {
        if (item != null)
            _repository.UpdateShoppingListItemAsync(item);
    }

}
