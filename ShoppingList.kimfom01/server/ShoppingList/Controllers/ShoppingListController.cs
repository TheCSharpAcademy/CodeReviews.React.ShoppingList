using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Dtos;
using ShoppingList.Entities;
using ShoppingList.Exceptions;
using ShoppingList.Services;

namespace ShoppingList.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly ILogger<ShoppingListController> _logger;
    private readonly IShoppingListService _shoppingListService;

    public ShoppingListController(
        ILogger<ShoppingListController> logger,
        IShoppingListService shoppingListService
    )
    {
        _logger = logger;
        _shoppingListService = shoppingListService;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ShoppingListItemDto>> GetAll()
    {
        _logger.LogInformation("Getting all the items");
        try
        {
            var items = await _shoppingListService.GetItems();

            return Ok(items);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("An error occured: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    [HttpGet("{itemId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ShoppingListItemDto>> Get(Guid itemId)
    {
        _logger.LogInformation("Getting item={itemId}", itemId);
        try
        {
            var item = await _shoppingListService.GetItem(itemId);

            return Ok(item);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("An error occured: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<ShoppingListItemDto>> Post(ShoppingListItemDto shoppingListItem)
    {
        _logger.LogInformation("Adding new item");
        try
        {
            var added = await _shoppingListService.AddItem(shoppingListItem);

            return CreatedAtAction(nameof(Get), new { itemId = added.Id }, added);
        }
        catch (Exception exception)
        {
            _logger.LogError("An error occured: {exception}", exception);
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{itemId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid itemId)
    {
        _logger.LogInformation("Deleting item={itemId}", itemId);
        try
        {
            await _shoppingListService.DeleteItem(itemId);

            return NoContent();
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("An error occured: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    [HttpPatch("{itemId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Patch(Guid itemId)
    {
        _logger.LogInformation("Updating item status");
        try
        {
            await _shoppingListService.MarkAsPicked(itemId);

            return NoContent();
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("An error occured: {exception}", exception);
            return NotFound(exception.Message);
        }
    }
}