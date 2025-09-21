using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; }= string.Empty;
        [Required]
        [Range(0.01,double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "stock must be non-negative ")]
        public int StockQuantity { get; set; }
        [StringLength(50)]

        public string Category { get; set; } = string.Empty;
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
