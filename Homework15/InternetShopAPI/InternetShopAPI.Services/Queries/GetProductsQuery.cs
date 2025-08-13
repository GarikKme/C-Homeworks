using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Queries;

public class GetProductsQuery : IRequestHandler<IList<ProductResponse>>
{
    private readonly InternetShopContext _context;

    public GetProductsQuery(InternetShopContext context)
    {
        _context = context;
    }

    public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Title = x.Title,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate
            })
            .OrderByDescending(x => x.ProductId)
            .ToListAsync(cancellationToken);
    }
}