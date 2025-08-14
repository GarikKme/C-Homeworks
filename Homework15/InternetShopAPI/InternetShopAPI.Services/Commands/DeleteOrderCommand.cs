using InternetShopAPI.Data.Context;
using InternetShopAPI.Services;
using Microsoft.EntityFrameworkCore;

public class DeleteOrderCommand
{
    public int OrderId { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly InternetShopContext _context;

    public DeleteOrderCommandHandler(InternetShopContext context) => _context = context;

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

        if (order == null) return false;

        _context.OrderItems.RemoveRange(order.OrderItems);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}