using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductsService productsService) : ControllerBase
    {
        private readonly IProductsService _productsService = productsService;

        [HttpGet]
        public async Task<ActionResult<PaginatedProducts>> GetProducts(int page, int pageSize)
        {
            var products = await _productsService.GetAsync(page, pageSize);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _productsService.AddAsync(product);
            return CreatedAtAction(nameof(AddProduct), product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            await _productsService.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
