namespace InternetShopAPI.Contract.Requests;

public class PatchProductRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public decimal? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
}