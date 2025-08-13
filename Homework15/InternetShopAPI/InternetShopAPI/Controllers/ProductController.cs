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
}

