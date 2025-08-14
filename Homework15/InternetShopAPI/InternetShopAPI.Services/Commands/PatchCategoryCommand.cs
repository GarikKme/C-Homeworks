using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands
{
    public class PatchCategoryCommand
    {
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class PatchCategoryCommandHandler : IRequestHandler<PatchCategoryCommand, CategoryResponse>
    {
        private readonly InternetShopContext _dbContext;

        public PatchCategoryCommandHandler(InternetShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryResponse> Handle(PatchCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

            if (category == null)
                throw new KeyNotFoundException($"Category with id {request.CategoryId} not found.");

            // Обновляем только те поля, что пришли не null
            if (request.Title != null)
                category.Title = request.Title;

            if (request.Description != null)
                category.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Description = category.Description
            };
        }
    }
}