using Products.DTO;
using Products.Models;
using Products.Repositories;

namespace Products.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _repository;
        public readonly IProductIdGenerator _idGenerator;
        public ProductService(IProductRepository repository, IProductIdGenerator idGenerator)
        {
                _repository = repository;
            _idGenerator = idGenerator;
        }
        public async Task<ProductDto?> AddToStockAsync(int id, int quantity)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;
            if (quantity <=0)
                throw new InvalidOperationException("Quantity must be greater than zero");
            var newStockQuantity = existingProduct.StockQuantity + quantity;
            var updatedProduct = await _repository.UpdateStockAsync(id, newStockQuantity);
            return updatedProduct == null ? null : MapToDto(updatedProduct);
        }

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            { 
            Id = _idGenerator.GenerateUniqueId(),
            Name = createProductDto.Name,
            Description = createProductDto.Description,
             Price = createProductDto.Price,
              StockQuantity = createProductDto.StockQuantity,
              Category = createProductDto.Category,
              Brand = createProductDto.Brand,
               IsActive = true,
               CreatedAt  = DateTime.UtcNow
            };
            var createdProduct = await _repository.CreateAsync(product);
            return MapToDto(createdProduct);
        }

        public async Task<ProductDto?> DecrementStockAsync(int id, int quantity)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;
            if (existingProduct.StockQuantity < quantity)
                throw new InvalidOperationException("Insfficient stock quantity");
            var newStockQuantity = existingProduct.StockQuantity - quantity;
            var updatedProduct = await _repository.UpdateStockAsync(id, newStockQuantity);
            return updatedProduct == null ? null : MapToDto(updatedProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var product = await _repository.GetAllAsync();
            return product.Select(p=> MapToDto(p));
            
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                Brand = product.Brand,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                IsActive = product.IsActive,
            };
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity,
                Category = createProductDto.Category,
                Brand = createProductDto.Brand
            };
            var updatedProduct = await _repository.UpdateAsync(id,product);
            return updatedProduct == null ? null : MapToDto(updatedProduct);
        }
    }
}
