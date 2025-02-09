using Microsoft.AspNetCore.Mvc;
using SimpleCleanArch.Api.Presenters;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController(
    ICreateProduct createProduct, IGetProduct getProduct,
    IDeleteProduct deleteProduct, IUpdateProduct updateProduct
) : ControllerBase
{
    private readonly ICreateProduct _createProduct = createProduct;
    private readonly IGetProduct _getProduct = getProduct;
    private readonly IDeleteProduct _deleteProduct = deleteProduct;
    private readonly IUpdateProduct _updateProduct = updateProduct;

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
        catch (Exception error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, output);
        }
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<GetProductOutput>> Get(long id)
    {
        try
        {
            var output = await _getProduct.Execute(id);
            return Ok(output);
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

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(long id, UpdateProductInput input)
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
    public async Task<ActionResult> Delete(long id)
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
}