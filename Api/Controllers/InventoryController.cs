using Microsoft.AspNetCore.Mvc;
using SimpleCleanArch.Api.Presenters;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("inventories")]
public class InventoryController(
    ICreateInventory createInventory
) : ControllerBase
{
    private readonly ICreateInventory _createInventory = createInventory;

    [HttpPost]
    public async Task<ActionResult<CreateInventoryOutput>> Post(CreateInventoryInput input)
    {
        try
        {
            var output = await _createInventory.Execute(input);
            return new CreatedAtRouteResult("GetInventory", new { id = output.InventoryId }, output);
        }
        catch (DomainException error)
        {
            var output = ErrorPresenter.GenerateJson(error.Message);
            return UnprocessableEntity(output);
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