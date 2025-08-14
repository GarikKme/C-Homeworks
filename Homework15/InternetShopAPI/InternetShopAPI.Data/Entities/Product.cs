
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

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        // --- Связь с Category ---
        public int CategoryId { get; set; } // FK
        public Category Category { get; set; } = null!; // навигационное свойство
}