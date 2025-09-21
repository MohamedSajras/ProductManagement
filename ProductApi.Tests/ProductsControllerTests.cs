using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Controllers;
using Products.DTO;
using Products.Services;
using Xunit;

namespace ProductApi.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetAllProducts_ReturnsOkResultWithProducts()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.GetAllProductAsync())
                .ReturnsAsync(new List<ProductDto> { new ProductDto { Id = 1, Name = "Test" } });

            var controller = new ProductsController(mockService.Object);

            var result = await controller.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Single(products);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResultWithProduct()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.GetProductByIdAsync(1))
                .ReturnsAsync(new ProductDto { Id = 1, Name = "Test" });

            var controller = new ProductsController(mockService.Object);

            var result = await controller.GetProduct(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtAction()
        {
            var mockService = new Mock<IProductService>();
            var dto = new CreateProductDto { Name = "New Product" };
            var created = new ProductDto { Id = 1, Name = "New Product" };

            mockService.Setup(s => s.CreateProductAsync(dto)).ReturnsAsync(created);

            var controller = new ProductsController(mockService.Object);
            controller.ModelState.Clear(); 

            var result = await controller.CreateProduct(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var product = Assert.IsType<ProductDto>(createdResult.Value);
            Assert.Equal("New Product", product.Name);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult()
        {
            var mockService = new Mock<IProductService>();
            var updateDto = new UpdateProductDto { Name = "Updated" };
            var updated = new ProductDto { Id = 1, Name = "Updated" };

            mockService.Setup(s => s.UpdateProductAsync(1, updateDto)).ReturnsAsync(updated);

            var controller = new ProductsController(mockService.Object);
            controller.ModelState.Clear();

            var result = await controller.UpdateProduct(1, updateDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal("Updated", product.Name);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.DeleteProductAsync(1)).ReturnsAsync(true);

            var controller = new ProductsController(mockService.Object);

            var result = await controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DecrementStock_ReturnsOk()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.DecrementStockAsync(1, 5)).ReturnsAsync(new ProductDto { Id = 1 });

            var controller = new ProductsController(mockService.Object);

            var result = await controller.DecrementStock(1, 5);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddToStock_ReturnsOk()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.AddToStockAsync(1, 10)).ReturnsAsync(new ProductDto { Id = 1 });

            var controller = new ProductsController(mockService.Object);

            var result = await controller.AddToStock(1, 10);

            Assert.IsType<OkResult>(result);
        }

    }
}