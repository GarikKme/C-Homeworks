
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopAPI.Data.Entities;

[Table("Product")]
public class Product
{
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = 0m;

        [Required]
        public DateTime ReleaseDate { get; set; }

        // --- Связь с Category ---
        public int CategoryId { get; set; } // FK
        public Category Category { get; set; } = null!; // навигационное свойство
}