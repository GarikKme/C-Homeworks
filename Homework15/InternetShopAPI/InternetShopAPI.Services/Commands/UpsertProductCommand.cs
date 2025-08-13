using InternetShopAPI.Data.Entities;

namespace InternetShopAPI.Services.Commands;

public class UpsertProductCommand
{
    public int ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime ReleaseDate { get; set; }

    public Product UpsertProduct()
    {
        var product =  new Product
        {
            ProductId = ProductId,
            Title = Title,
            Description = Description,
            ReleaseDate = ReleaseDate
        };
        return product;
    }
}