using Microsoft.AspNetCore.Mvc;
using ShoppingList.StanimalTheMan.Server.Models;
using ShoppingList.StanimalTheMan.Server.Services;

namespace ShoppingList.StanimalTheMan.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListItemController : Controller
{
	private readonly IShoppingListItemService _shoppingListItemService;

	public ShoppingListItemController(IShoppingListItemService shoppingListItemService)
	{
		_shoppingListItemService = shoppingListItemService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ShoppingListItem>>> GetShoppingListItems()
	{
		try
		{
			var shoppingListItems = await _shoppingListItemService.GetAllShoppingListItemsAsync();
			return Ok(shoppingListItems);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ShoppingListItem>> GetShoppingListItemById(int id)
	{
		try
		{
			var shoppingListItem = await _shoppingListItemService.GetShoppingListItemByIdAsync(id);
			if (shoppingListItem == null)
			{
				return NotFound();
			}

			return Ok(shoppingListItem);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpPost]
	public async Task<ActionResult<ShoppingListItem>> AddShoppingListItem(ShoppingListItem shoppingListItem)
	{
		try
		{
			await _shoppingListItemService.AddShoppingListItemAsync(shoppingListItem);
			return CreatedAtAction(nameof(GetShoppingListItemById), new { id = shoppingListItem.Id }, shoppingListItem);
		} catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateShoppingListItem(int id, ShoppingListItem shoppingListItem)
	{
		if (id != shoppingListItem.Id)
		{
			return BadRequest("Shopping List Item ID mismatch");
		}

		try
		{
			await _shoppingListItemService.UpdateShoppingListItemAsync(shoppingListItem);
			return NoContent();
		}
		catch (KeyNotFoundException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteShoppingListItem(int id)
	{
		try {
			var shoppingListItem = await _shoppingListItemService.GetShoppingListItemByIdAsync(id);
			if (shoppingListItem == null)
			{
				return NotFound();
			}

			await _shoppingListItemService.DeleteShoppingListItemAsync(shoppingListItem);
			return NoContent();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}
}
