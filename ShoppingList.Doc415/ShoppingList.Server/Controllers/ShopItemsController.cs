using Microsoft.AspNetCore.Mvc;
using ShoppingList.Server.Models;
using ShoppingList.Server.Repositories;

namespace ShoppingList.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopItemsController : ControllerBase
{
    private readonly IShoppingRepository _repository;
    public ShopItemsController(IShoppingRepository repository)
    {
        _repository = repository;
    }

    // GET: api/ShopItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShopItem>>> GetShopItems()
    {
        return await _repository.GetItems();
    }

    // GET: api/ShopItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShopItem>> GetShopItem(int id)
    {
        var shopItem = await _repository.GetItemById(id);

        if (shopItem == null)
        {
            return NotFound();
        }

        return shopItem;
    }

    // PUT: api/ShopItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShopItem(int id, ShopItem shopItem)
    {
        if (id != shopItem.Id)
        {
            return BadRequest();
        }

        await _repository.UpdateItem(shopItem);
        return NoContent();
    }

    // POST: api/ShopItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ShopItem>> PostShopItem(ShopItem shopItem)
    {
        var toAdd = new ShopItem() { Name = shopItem.Name };
        var newId=await _repository.AddItem(toAdd);
        shopItem.Id = newId;
        return CreatedAtAction("GetShopItem", new { id = newId }, shopItem);
    }

    // DELETE: api/ShopItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShopItem(int id)
    {
        try
        {
            _repository.DeleteItem(id);
            return Ok();
        }
        catch
        {
            return NoContent();
        }
    }

}
