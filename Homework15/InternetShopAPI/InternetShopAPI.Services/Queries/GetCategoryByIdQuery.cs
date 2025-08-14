using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<int, CategoryResponse>
{
    private readonly InternetShopContext _context;
    public GetCategoryByIdQueryHandler(InternetShopContext context) => _context = context;

    public async Task<CategoryResponse?> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(c => c.CategoryId == categoryId)
            .Select(c => new CategoryResponse
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
