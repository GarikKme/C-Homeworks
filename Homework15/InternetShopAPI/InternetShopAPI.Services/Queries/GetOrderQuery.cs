using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Services;
using Microsoft.EntityFrameworkCore;

public class GetOrderQuery
{
    public int OrderId { get; set; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly InternetShopContext _context;

    public GetOrderQueryHandler(InternetShopContext context) => _context = context;

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

        if (order == null) throw new KeyNotFoundException("Order not found");

        return new OrderResponse
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            CustomerEmail = order.CustomerEmail,
            Status = order.Status,
            OrderDate = order.OrderDate
        };
    }
}