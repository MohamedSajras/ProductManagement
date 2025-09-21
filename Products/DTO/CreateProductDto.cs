using System.ComponentModel.DataAnnotations;

namespace Products.DTO
{
    public class CreateProductDto
    {
        
        [Required]
        [StringLength(100, ErrorMessage ="Name cannot be longer than 100 character")]
        public string Name { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "Description cannot be longer than 100 character")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "stock must be non-negative ")]
        public int StockQuantity { get; set; }
        [StringLength(50)]

        public string Category { get; set; } = string.Empty;
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        
    }

    public class UpdateProductDto
    {

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 character")]
        public string Name { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "Description cannot be longer than 100 character")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "stock must be non-negative ")]
        public int StockQuantity { get; set; }
        [StringLength(50)]

        public string Category { get; set; } = string.Empty;
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

    }

    public class ProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
       

        public string Category { get; set; } = string.Empty;
        
        public string Brand { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

    }
}
