using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.StevieTV.Database;
using ShoppingList.StevieTV.Models;

namespace ShoppingList.StevieTV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListContext _context;

        public ShoppingListController(ShoppingListContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListItem>>> GetShoppingListItems()
        {
            return await _context.ShoppingListItems.ToListAsync();
        }

        // GET: api/ShoppingList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListItem>> GetShoppingListItems(int id)
        {
            var shoppingListItems = await _context.ShoppingListItems.FindAsync(id);

            if (shoppingListItems == null)
            {
                return NotFound();
            }

            return shoppingListItems;
        }

        // PUT: api/ShoppingList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingListItems(int id, ShoppingListItem shoppingListItem)
        {
            if (id != shoppingListItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingListItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingListItem>> PostShoppingListItems(ShoppingListItem shoppingListItem)
        {
            _context.ShoppingListItems.Add(shoppingListItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingListItems", new { id = shoppingListItem.Id }, shoppingListItem);
        }

        // DELETE: api/ShoppingList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingListItems(int id)
        {
            var shoppingListItems = await _context.ShoppingListItems.FindAsync(id);
            if (shoppingListItems == null)
            {
                return NotFound();
            }

            _context.ShoppingListItems.Remove(shoppingListItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingListItemsExists(int id)
        {
            return _context.ShoppingListItems.Any(e => e.Id == id);
        }
    }
}
