using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext ;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {

          _productDbContext.Products.Add(product);
          await _productDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingProduct = await _productDbContext.Products.FindAsync(id);
            if (existingProduct == null)
                return false;
            _productDbContext.Products.Remove(existingProduct);
            await _productDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await  _productDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        { 
            var existingProduct = await _productDbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
            existingProduct.Brand = product.Brand;
            existingProduct.UpdatedAt = DateTime.UtcNow;
            await _productDbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product?> UpdateStockAsync(int id, int newStockquantity)
        {
            var existingProduct = await _productDbContext.Products.FindAsync(id);
            if (existingProduct == null)
                return null;
            existingProduct.StockQuantity = newStockquantity;
            existingProduct.UpdatedAt = DateTime.UtcNow;
            await _productDbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
