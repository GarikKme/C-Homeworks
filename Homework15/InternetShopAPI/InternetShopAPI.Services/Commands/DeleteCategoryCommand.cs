using InternetShopAPI.Data.Context;

namespace InternetShopAPI.Services.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<int, bool>
{
    private readonly InternetShopContext _context;
    public DeleteCategoryCommandHandler(InternetShopContext context) => _context = context;

    public async Task<bool> Handle(int categoryId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Categories.FindAsync(new object[] { categoryId }, cancellationToken);
        if (entity == null) return false;

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}