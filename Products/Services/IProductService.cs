using Products.DTO;

namespace Products.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto createProductDto);
        Task<bool> DeleteProductAsync(int id);
        Task<ProductDto?> DecrementStockAsync(int id, int quantity);
        Task<ProductDto?> AddToStockAsync(int id, int quantity);

    }
}
