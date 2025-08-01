using System.ComponentModel.DataAnnotations;

namespace Homework13.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
    
        [Required]
        [Display(Name = "User Name")]
        public string? Name { get; set; }
        
    
        [Required]
        [Display(Name = "User Email")]
        public string? Email { get; set; }
    
        [Required]
        [Display(Name = "User Phone")]
        public string? Phone { get; set; }
        
        [Required]
        [Display(Name = "User Work Phone")]
        public string? WorkPhone { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}

