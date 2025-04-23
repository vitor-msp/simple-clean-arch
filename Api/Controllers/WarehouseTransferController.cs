using Microsoft.AspNetCore.Mvc;
using SimpleCleanArch.Api.Presenters;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("warehouse-transfers")]
public class WarehouseTransferController(
    ICreateWarehouseTransfer createWarehouseTransfer
) : ControllerBase
{
    private readonly ICreateWarehouseTransfer _createWarehouseTransfer = createWarehouseTransfer;

    [HttpPost]
    public async Task<ActionResult<CreateWarehouseTransferOutput>> Post(CreateWarehouseTransferInput input)
    {
        try
        {
            var output = await _createWarehouseTransfer.Execute(input);
            return new CreatedAtRouteResult("GetWarehouseTransfer", new { id = output.WarehouseTransferId }, output);
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

    [HttpGet(Name = "GetWarehouseTransfer")]
    public void GetFake(int id) => NoContent();
}