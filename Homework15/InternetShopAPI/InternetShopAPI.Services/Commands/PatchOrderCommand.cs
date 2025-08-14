using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Services;
using Microsoft.EntityFrameworkCore;

public class PatchOrderCommand
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string Status { get; set; } = "Pending";
}

public class PatchOrderCommandHandler : IRequestHandler<PatchOrderCommand, OrderResponse>
{
    private readonly InternetShopContext _context;

    public PatchOrderCommandHandler(InternetShopContext context) => _context = context;

    public async Task<OrderResponse> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);
        if (order == null) throw new KeyNotFoundException("Order not found");

        order.Status = request.Status;
        await _context.SaveChangesAsync(cancellationToken);

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