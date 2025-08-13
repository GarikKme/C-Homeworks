using System.ComponentModel.DataAnnotations;

namespace InternetShopAPI.Contract.Requests;

public class UpsertProductRequest
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }
}