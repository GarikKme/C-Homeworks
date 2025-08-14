using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands;

public class UpsertCategoryCommand
{
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Category UpsertCategory() => new Category
    {
        CategoryId = CategoryId,
        Title = Title,
        Description = Description
    };
}

public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, CategoryResponse>
{
    private readonly InternetShopContext _context;
    public UpsertCategoryCommandHandler(InternetShopContext context) => _context = context;

    public async Task<CategoryResponse> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Categories
                         .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken)
                     ?? new Category();

        entity.Title = request.Title;
        entity.Description = request.Description;

        if (entity.CategoryId == 0)
            _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CategoryResponse
        {
            CategoryId = entity.CategoryId,
            Title = entity.Title,
            Description = entity.Description
        };
    }
}