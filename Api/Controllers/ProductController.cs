using Microsoft.AspNetCore.Mvc;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;
using SimpleCleanArch.Api.Presenters;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly ICreateProduct _createProductUseCase;
    private readonly IGetProduct _getProductUseCase;
    private readonly IDeleteProduct _deleteProductuseCase;

    public ProductController(
        ICreateProduct createProductUseCase, IGetProduct getProductUseCase, IDeleteProduct deleteProductuseCase)
    {
        _createProductUseCase = createProductUseCase;
        _getProductUseCase = getProductUseCase;
        _deleteProductuseCase = deleteProductuseCase;
    }

    [HttpPost]
    public ActionResult<CreateProductOutput> Post(CreateProductInput input)
    {
        try
        {
            var output = _createProductUseCase.Execute(input);
            return Ok(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.From(error.Message);
            return BadRequest(output);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<GetProductOutput> Get(long id)
    {
        try
        {
            var output = _getProductUseCase.Execute(id);
            if (output == null) return NotFound();
            return Ok(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.From(error.Message);
            return BadRequest(output);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<GetProductOutput> Delete(long id)
    {
        try
        {
            var output = _deleteProductuseCase.Execute(id);
            if (output == null) return NotFound();
            return Ok(output);
        }
        catch (Exception error)
        {
            var output = ErrorPresenter.From(error.Message);
            return BadRequest(output);
        }
    }
}