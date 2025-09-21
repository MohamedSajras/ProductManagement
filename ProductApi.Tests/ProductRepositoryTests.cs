using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;
using Products.Repositories;

namespace ProductApi.Tests
{
    public class ProductRepositoryTests
    {
        private ProductDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ProductDbContext(options);
        }
        [Fact]
        public async Task CreateAsync_ShouldAddProduct()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);

            var product = new Product { Name = "Test", Price = 100, Id = 1234 };

            var result = await repo.CreateAsync(product);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Single(context.Products);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            var context = GetInMemoryDbContext();
            context.Products.AddRange(new Product { Name = "A", Id = 1234 }, new Product { Name = "B", Id = 23456 });            
            await context.SaveChangesAsync();

            var repo = new ProductRepository(context);

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectProduct()
        {
            var context = GetInMemoryDbContext();
            var product = new Product { Name = "A" };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repo = new ProductRepository(context);

            var result = await repo.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal("A", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            var context = GetInMemoryDbContext();
            var product = new Product { Name = "B" };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repo = new ProductRepository(context);

            var result = await repo.DeleteAsync(product.Id);

            Assert.True(result);
            Assert.Empty(context.Products);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseIfNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new ProductRepository(context);

            var result = await repo.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyProduct()
        {
            var context = GetInMemoryDbContext();
            var product = new Product { Name = "Old", Price = 50 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repo = new ProductRepository(context);
            var updated = new Product { Name = "New", Price = 100 };

            var result = await repo.UpdateAsync(product.Id, updated);

            Assert.NotNull(result);
            Assert.Equal("New", result.Name);
            Assert.Equal(100, result.Price);
        }

        [Fact]
        public async Task UpdateStockAsync_ShouldChangeStockQuantity()
        {
            var context = GetInMemoryDbContext();
            var product = new Product { Name = "Stocky", StockQuantity = 10 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repo = new ProductRepository(context);

            var result = await repo.UpdateStockAsync(product.Id, 25);

            Assert.NotNull(result);
            Assert.Equal(25, result.StockQuantity);
        }

    }
}
