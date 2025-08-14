using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands
{
    public class PutProductCommand
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class PutProductCommandHandler : IRequestHandler<PutProductCommand, ProductResponse>
    {
        private readonly InternetShopContext _dbContext;

        public PutProductCommandHandler(InternetShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(PutProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with id {request.ProductId} not found.");

            // Полное обновление всех обязательных полей
            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.ReleaseDate = request.ReleaseDate;

            await _dbContext.SaveChangesAsync(cancellationToken);

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