using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Dtos;
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

    /// <summary>
    /// Gets all the items available in database.
    /// </summary>
    /// <returns>A list of available items in database</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/shoppinglist
    ///
    /// </remarks>
    /// <response code="200">Returns all the items available in database.</response>
    /// <response code="404">If there are no available items in the database</response>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<ShoppingItemResponse>>> GetAll()
    {
        _logger.LogInformation("Getting all the items");
        try
        {
            var items = await _shoppingListService.GetItems();

            return Ok(items);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("Error getting items: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Gets an item from the database.
    /// </summary>
    /// <param name="itemId">The id of the specific item</param>
    /// <returns>The item with the specified itemId</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/shoppinglist/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <response code="200">Returns an item with the specified itemId.</response>
    /// <response code="404">If no item match the specified itemId</response>
    [HttpGet("{itemId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ShoppingItemResponse>> Get(Guid itemId)
    {
        _logger.LogInformation("Getting item={itemId}", itemId);
        try
        {
            var item = await _shoppingListService.GetItem(itemId);

            return Ok(item);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("Error getting items: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Adds a new item to the existing list of items
    /// </summary>
    /// <param name="request">The item to add</param>
    /// <returns>A http status created result.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/shoppinglist
    ///     Content-Type: application/json
    ///     {
    ///         "isPickedUp" : false,
    ///         "item" : "Cookies"
    ///     }
    ///
    /// The request must be made with 'application/json' content type.
    /// </remarks>
    /// <response code="201">If successfully added</response>
    /// <response code="400">If there was an error while adding.</response>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingItemResponse>> Post(CreateShoppingItem request)
    {
        _logger.LogInformation("Adding new item");
        try
        {
            var added = await _shoppingListService.AddItem(request);

            return CreatedAtAction(nameof(Get), new { itemId = added.Id }, added);
        }
        catch (Exception exception)
        {
            _logger.LogError("Error creating item: {exception}", exception);
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Deletes the item with the specified itemId
    /// </summary>
    /// <param name="itemId">The item to delete</param>
    /// <returns>A http status no content result.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/shoppinglist/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <response code="204">If successfully deleted</response>
    /// <response code="404">If the item doesn't exist.</response>
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
            _logger.LogError("Error deleting item: {exception}", exception);
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Updates the picked status of the item with the specified itemId
    /// </summary>
    /// <param name="itemId">The item to update</param>
    /// <returns>A http status no content result.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /api/shoppinglist/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <response code="204">If successfully updated</response>
    /// <response code="404">If the item doesn't exist.</response>
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
            _logger.LogError("Error updating item: {exception}", exception);
            return NotFound(exception.Message);
        }
    }
}