using Microsoft.AspNetCore.Mvc;
using Products.DTO;
using Products.Services;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var products = await _productService.GetProductByIdAsync(id);
            return Ok(products);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var product = await _productService.CreateProductAsync(createProductDto);
                
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id}, product);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var product = await _productService.UpdateProductAsync(id, updateProductDto);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        { 
            
                var success = await _productService.DeleteProductAsync(id);
                if (!success)
                    return NotFound();
            return NoContent();
            
        }

        [HttpPut("decremet-stok/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {

            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero");
            try {
                var product = await _productService.DecrementStockAsync(id,quantity);
                if (product == null)
                    return NotFound();
                return Ok(); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("add-to-stok/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {

            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero");
            try
            {
                var product = await _productService.AddToStockAsync(id, quantity);
                if (product == null)
                    return NotFound();
                return Ok();
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
