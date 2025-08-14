using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Services;
using InternetShopAPI.Services.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAPI.Controllers;
//[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
    {
        return Ok(await getProductsQuery.Handle());
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse> getProductByIdQuery)
    {
        return Ok(await getProductByIdQuery.Handle(productId));
    }

    [HttpPost]
    public async Task<IActionResult> UpserProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductCommand, [FromBody] UpsertProductRequest request)
    {
        var product = await upsertProductCommand.Handle(new UpsertProductCommand
        {
            ProductId = request.ProductId,
            Title = request.Title,
            Description = request.Description,
            ReleaseDate = request.ReleaseDate
        });

        return Ok(product);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> PutProductAsync(
        int productId,
        [FromServices] IRequestHandler<PutProductCommand, ProductResponse> putProductHandler,
        [FromBody] UpsertProductRequest request)
    {
        if (productId != request.ProductId)
            return BadRequest("Route id and body ProductId must match.");

        var result = await putProductHandler.Handle(new PutProductCommand
        {
            ProductId = request.ProductId,
            Title = request.Title,
            Description = request.Description,
            ReleaseDate = request.ReleaseDate
        });

        return Ok(result);
    }



    [HttpPatch("{productId}")]
    public async Task<IActionResult> PatchProductAsync(
        int productId,
        [FromServices] IRequestHandler<PatchProductCommand, ProductResponse> patchProductCommand,
        [FromBody] PatchProductRequest request)
    {
        var product = await patchProductCommand.Handle(new PatchProductCommand
        {
            ProductId = productId,
            Title = request.Title,            // можно null — значит не менять
            Description = request.Description,
            ReleaseDate = request.ReleaseDate
        });

        return Ok(product);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProductAsync(
        int productId,
        [FromServices] IRequestHandler<int, bool> deleteProductCommand)
    {
        var success = await deleteProductCommand.Handle(productId);
        if (!success)
            return NotFound();

        return NoContent();
    }
}

