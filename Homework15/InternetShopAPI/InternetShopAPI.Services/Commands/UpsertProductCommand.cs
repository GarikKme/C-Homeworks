using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands;

public class UpsertProductCommand
{
    public int ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    public Product UpsertProduct()
    {
        var product =  new Product
        {
            ProductId = ProductId,
            Title = Title,
            Description = Description,
            Price = Price,
            ReleaseDate = ReleaseDate
        };
        return product;
    }
}

public class UpsertMovieCommandHandler : IRequestHandler<UpsertProductCommand, ProductResponse>
{
    private readonly InternetShopContext _context;

    public UpsertMovieCommandHandler(InternetShopContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse> Handle(UpsertProductCommand request, CancellationToken cancellationToken = default)
    {
        var movie = await GetMovieAsync(request.ProductId, cancellationToken);

        if (movie == null)
        {
            movie = request.UpsertProduct();
            await _context.AddAsync(movie, cancellationToken);
        }

        movie.Title = request.Title;
        movie.Description = request.Description;
        movie.ReleaseDate = request.ReleaseDate;

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductResponse
        {
            ProductId = movie.ProductId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            ReleaseDate = request.ReleaseDate
        };
    }

    private async Task<Product> GetMovieAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }
}