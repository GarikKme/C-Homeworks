using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopAPI.Contract.Requests;

public class UpsertOrderRequest
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(255)]
    public string CustomerName { get; set; }

    [Required]
    [StringLength(255)]
    public string CustomerEmail { get; set; }

    [StringLength(20)]
    public string? Status { get; set; } = "Pending";
}