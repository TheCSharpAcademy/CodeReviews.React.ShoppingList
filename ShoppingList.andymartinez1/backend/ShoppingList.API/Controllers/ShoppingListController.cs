using Microsoft.AspNetCore.Mvc;
using ShoppingList.ServiceContracts;
using ShoppingList.ServiceContracts.DTO;

namespace ShoppingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly IGroceryService _service;

    public ShoppingListController(IGroceryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var items = await _service.GetAllAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> Details(int id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GroceryItemAddRequest request)
    {
        var item = await _service.AddAsync(request);

        return CreatedAtAction(nameof(Details), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int id, [FromBody] GroceryItemUpdateRequest request)
    {
        var item = await _service.UpdateAsync(request);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        await _service.DeleteAsync(id);

        return NoContent();
    }
}