using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Contract;

public interface ICreateInventory
{
    Task<CreateInventoryOutput> Execute(CreateInventoryInput input);
}

public class CreateInventoryInput
{
    [Required(ErrorMessage = "warehouse is required")]
    public int WarehouseId { get; set; }

    [Required(ErrorMessage = "product id is required")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "quantity is required")]
    public int Quantity { get; set; }

    public IInventory GetEntity()
        => new Inventory()
        {
            WarehouseId = WarehouseId,
            ProductId = ProductId,
            Quantity = Quantity,
        };
}

public class CreateInventoryOutput
{
    public required int InventoryId { get; init; }
}