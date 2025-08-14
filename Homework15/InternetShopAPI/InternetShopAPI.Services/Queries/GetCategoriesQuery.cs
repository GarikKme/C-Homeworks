using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<IList<CategoryResponse>>
{
    private readonly InternetShopContext _context;
    public GetCategoriesQueryHandler(InternetShopContext context) => _context = context;

    public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Select(c => new CategoryResponse
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description
            })
            .ToListAsync(cancellationToken);
    }
}