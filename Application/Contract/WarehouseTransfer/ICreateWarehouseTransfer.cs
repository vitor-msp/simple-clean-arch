using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Contract;

public interface ICreateWarehouseTransfer
{
    Task<CreateWarehouseTransferOutput> Execute(CreateWarehouseTransferInput input);
}

public class CreateWarehouseTransferInput
{
    [Required(ErrorMessage = "source warehouse id is required")]
    public Guid SourceWarehouseId { get; set; }

    [Required(ErrorMessage = "target warehouse id is required")]
    public Guid TargetWarehouseId { get; set; }

    [Required(ErrorMessage = "product id is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "product quantity is required")]
    public int ProductQuantity { get; set; }

    public IWarehouseTransfer GetEntity()
        => new WarehouseTransfer()
        {
            SourceWarehouseId = SourceWarehouseId,
            TargetWarehouseId = TargetWarehouseId,
            ProductId = ProductId,
            ProductQuantity = ProductQuantity,
        };
}

public class CreateWarehouseTransferOutput
{
    public required Guid WarehouseTransferId { get; init; }
}