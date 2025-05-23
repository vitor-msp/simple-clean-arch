using Microsoft.AspNetCore.Mvc;
using Query.Contract;
using SimpleCleanArch.Api.Presenters;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController(
    ICreateProduct createProduct,
    IDeleteProduct deleteProduct,
    IUpdateProduct updateProduct,
    IProductQuery productQuery
) : ControllerBase
{
    private readonly ICreateProduct _createProduct = createProduct;
    private readonly IDeleteProduct _deleteProduct = deleteProduct;
    private readonly IUpdateProduct _updateProduct = updateProduct;
    private readonly IProductQuery _productQuery = productQuery;

    [HttpPost]
    public async Task<ActionResult<CreateProductOutput>> Post(CreateProductInput input)
    {
        try
        {
            var output = await _createProduct.Execute(input);
            return new CreatedAtRouteResult("GetProduct", new { id = output.ProductId }, output);
        }
        catch (DomainException error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return UnprocessableEntity(output);
        }
        catch (ConflictException error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return Conflict(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id, UpdateProductInput input)
    {
        try
        {
            await _updateProduct.Execute(id, input);
            return NoContent();
        }
        catch (NotFoundException error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return NotFound(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _deleteProduct.Execute(id);
            return NoContent();
        }
        catch (NotFoundException error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return NotFound(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult> Get(int id)
    {
        try
        {
            var product = await _productQuery.GetById(id);
            return product is null ? NotFound() : Ok(product);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetMany(
        [FromQuery] double? minPrice, [FromQuery] double? maxPrice, [FromQuery] string? category,
        [FromQuery] int? skip, [FromQuery] int? limit, [FromQuery] string? orderBy, [FromQuery] bool? orderAsc
    )
    {
        try
        {
            var input = new GetProductsInput(skip, limit, orderBy, orderAsc)
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Category = category,
            };
            var products = await _productQuery.GetMany(input);
            return Ok(products);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }
}