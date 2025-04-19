using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class CreateInventory(
    IInventoryRepository inventoryRepository,
    IProductRepository productRepository,
    IWarehouseRepository warehouseRepository
) : ICreateInventory
{
    private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;

    public async Task<CreateInventoryOutput> Execute(CreateInventoryInput input)
    {
        var product = await _productRepository.Get(input.ProductId)
            ?? throw new NotFoundException($"Product id {input.ProductId} not found.");
        var warehouse = await _warehouseRepository.GetById(input.WarehouseId)
            ?? throw new NotFoundException($"Warehouse id {input.WarehouseId} not found.");

        var inventory = input.GetEntity();
        await _inventoryRepository.Create(inventory);
        await _inventoryRepository.Commit();
        return new() { InventoryId = inventory.Id };
    }
}