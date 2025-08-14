using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Services;
using InternetShopAPI.Services.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAPI.Controllers;

[Route("v{version:apiVersion}/[Controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrdersQuery)
        => Ok(await getOrdersQuery.Handle());

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse> getOrderByIdQuery)
        => Ok(await getOrderByIdQuery.Handle(orderId));

    [HttpPost]
    public async Task<IActionResult> UpsertOrderAsync(
        [FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> upsertOrderCommand,
        [FromBody] UpsertOrderRequest request)
    {
        var result = await upsertOrderCommand.Handle(new UpsertOrderCommand
        {
            OrderId = request.OrderId,
            OrderDate = request.OrderDate,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            Status = request.Status
        });
        return Ok(result);
    }

    [HttpPut("{orderId}")]
    public async Task<IActionResult> PutOrderAsync(
        int orderId,
        [FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> putHandler,
        [FromBody] UpsertOrderRequest request)
    {
        if (orderId != request.OrderId)
            return BadRequest("Route id and body OrderId must match.");

        var result = await putHandler.Handle(new UpsertOrderCommand
        {
            OrderId = request.OrderId,
            OrderDate = request.OrderDate,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            Status = request.Status
        });
        return Ok(result);
    }



    [HttpPatch("{orderId}")]
    public async Task<IActionResult> PatchOrderAsync(
        int orderId,
        [FromServices] IRequestHandler<PatchOrderCommand, OrderResponse> patchHandler,
        [FromBody] PatchOrderRequest request)
    {
        var result = await patchHandler.Handle(new PatchOrderCommand
        {
            OrderId = orderId,
            OrderDate = request.OrderDate,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            Status = request.Status
        });
        return Ok(result);
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrderAsync(int orderId, [FromServices] IRequestHandler<int, bool> deleteHandler)
    {
        var success = await deleteHandler.Handle(orderId);
        if (!success) return NotFound();
        return NoContent();
    }
}
