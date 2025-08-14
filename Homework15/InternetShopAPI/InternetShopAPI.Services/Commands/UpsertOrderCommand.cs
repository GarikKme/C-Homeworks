using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Data.Context;
using InternetShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAPI.Services.Commands;

public class UpsertOrderCommand
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string? Status { get; set; } = "Pending";
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public List<OrderItemDto> Items { get; set; } = new();

    public Order ToEntity()
    {
        return new Order
        {
            OrderId = OrderId,
            CustomerName = CustomerName,
            CustomerEmail = CustomerEmail,
            Status = Status,
            OrderDate = OrderDate,
            OrderItems = Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class UpsertOrderCommandHandler : IRequestHandler<UpsertOrderCommand, OrderResponse>
{
    private readonly InternetShopContext _context;

    public UpsertOrderCommandHandler(InternetShopContext context) => _context = context;

    public async Task<OrderResponse> Handle(UpsertOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

        if (order == null)
        {
            order = request.ToEntity();
            _context.Orders.Add(order);
        }
        else
        {
            order.CustomerName = request.CustomerName;
            order.CustomerEmail = request.CustomerEmail;
            order.Status = request.Status;
            order.OrderDate = request.OrderDate;

            _context.OrderItems.RemoveRange(order.OrderItems);
            order.OrderItems = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();
        }

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
