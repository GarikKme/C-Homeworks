using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands
{
    public class PutOrderCommand
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
    }

    public class PutOrderCommandHandler : IRequestHandler<PutOrderCommand, OrderResponse>
    {
        private readonly InternetShopContext _context;

        public PutOrderCommandHandler(InternetShopContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(PutOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");

            order.OrderDate = request.OrderDate;
            order.CustomerName = request.CustomerName;
            order.CustomerEmail = request.CustomerEmail;
            order.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                Status = order.Status
            };
        }
    }
}