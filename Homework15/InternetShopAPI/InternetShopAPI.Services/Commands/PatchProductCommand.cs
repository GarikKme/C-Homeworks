using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands
{
    public class PatchProductCommand
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }

    public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, ProductResponse>
    {
        private readonly InternetShopContext _dbContext;

        public PatchProductCommandHandler(InternetShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(PatchProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with id {request.ProductId} not found.");

            if (request.Title != null)
                product.Title = request.Title;

            if (request.Description != null)
                product.Description = request.Description;

            if (request.Price != null)
                product.Price = request.Price;

            if (request.ReleaseDate.HasValue)
                product.ReleaseDate = request.ReleaseDate.Value;

            await _dbContext.SaveChangesAsync();

            return new ProductResponse
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ReleaseDate = product.ReleaseDate
            };
        }
    }
}