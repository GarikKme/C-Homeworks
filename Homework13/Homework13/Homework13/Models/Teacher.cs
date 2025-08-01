using System.ComponentModel.DataAnnotations;

namespace Homework13.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
    
        [Required]
        [Display(Name = "Teacher Name")]
        public string? Name { get; set; }
        
    
        [Required]
        [Display(Name = "Teacher Email")]
        public string? Email { get; set; }
    
        [Required]
        [Display(Name = "Subject")]
        public string? Subject { get; set; }
    
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}

