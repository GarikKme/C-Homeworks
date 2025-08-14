using InternetShopAPI.Contract.Requests;
using InternetShopAPI.Services;
using InternetShopAPI.Services.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAPI.Controllers;

[Route("v{version:apiVersion}/[Controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        => Ok(await getCategoriesQuery.Handle());

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        => Ok(await getCategoryByIdQuery.Handle(categoryId));

    [HttpPost]
    public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertCategoryCommand, [FromBody] UpsertCategoryRequest request)
    {
        var result = await upsertCategoryCommand.Handle(new UpsertCategoryCommand
        {
            CategoryId = request.CategoryId,
            Title = request.Title,
            Description = request.Description
        });
        return Ok(result);
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> PutCategoryAsync(int categoryId, [FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> putHandler, [FromBody] UpsertCategoryRequest request)
    {
        if (categoryId != request.CategoryId)
            return BadRequest("Route id and body CategoryId must match.");

        var result = await putHandler.Handle(new UpsertCategoryCommand
        {
            CategoryId = request.CategoryId,
            Title = request.Title,
            Description = request.Description
        });
        return Ok(result);
    }

    [HttpPatch("{categoryId}")]
    public async Task<IActionResult> PatchCategoryAsync(int categoryId, [FromServices] IRequestHandler<PatchCategoryCommand, CategoryResponse> patchHandler, [FromBody] PatchCategoryRequest request)
    {
        var result = await patchHandler.Handle(new PatchCategoryCommand
        {
            CategoryId = categoryId,
            Title = request.Title,
            Description = request.Description
        });
        return Ok(result);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategoryAsync(int categoryId, [FromServices] IRequestHandler<int, bool> deleteHandler)
    {
        var success = await deleteHandler.Handle(categoryId);
        if (!success) return NotFound();
        return NoContent();
    }
}
