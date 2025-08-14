
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopAPI.Data.Entities;

[Table("Category")]
public class Category
{
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }


        public virtual ICollection<Product>? Products { get; set; }
}