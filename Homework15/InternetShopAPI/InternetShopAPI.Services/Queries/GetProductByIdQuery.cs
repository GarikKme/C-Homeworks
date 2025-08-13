using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<int, ProductResponse>
{
    private readonly InternetShopContext _context;
    public GetProductByIdQueryHandler(InternetShopContext context)
    {
        _context = context;
    }
    public async Task<ProductResponse?> Handle(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(x => x.ProductId == productId)
            .Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Title = x.Title,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}