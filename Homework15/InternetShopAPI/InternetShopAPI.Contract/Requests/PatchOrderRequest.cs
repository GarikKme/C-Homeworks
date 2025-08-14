namespace InternetShopAPI.Contract.Requests;

public class PatchOrderRequest
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public string CustomerName { get; set; }

    public string CustomerEmail { get; set; }

    public string? Status { get; set; } = "Pending";
}