using System.ComponentModel.DataAnnotations;

namespace InternetShopAPI.Contract.Requests;

public class UpsertCategoryRequest
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; }
}