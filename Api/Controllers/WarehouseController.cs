using Microsoft.AspNetCore.Mvc;
using SimpleCleanArch.Api.Presenters;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("warehouses")]
public class WarehouseController(
    ICreateWarehouse createWarehouse,
    IDeleteWarehouse deleteWarehouse,
    IUpdateWarehouse updateWarehouse
) : ControllerBase
{
    private readonly ICreateWarehouse _createWarehouse = createWarehouse;
    private readonly IDeleteWarehouse _deleteWarehouse = deleteWarehouse;
    private readonly IUpdateWarehouse _updateWarehouse = updateWarehouse;

    [HttpPost]
    public async Task<ActionResult<CreateWarehouseOutput>> Post(CreateWarehouseInput input)
    {
        try
        {
            var output = await _createWarehouse.Execute(input);
            return new CreatedAtRouteResult("GetWarehouse", new { id = output.WarehouseId }, output);
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


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _deleteWarehouse.Execute(id);
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

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id, UpdateWarehouseInput input)
    {
        try
        {
            await _updateWarehouse.Execute(id, input);
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

    [HttpGet(Name = "GetWarehouse")]
    public void GetFake(int id) => NoContent();
}