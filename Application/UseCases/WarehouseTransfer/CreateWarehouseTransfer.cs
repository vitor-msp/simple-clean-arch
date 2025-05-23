using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class CreateWarehouseTransfer(
    IWarehouseTransferRepository warehouseTransferRepository,
    IProductRepository productRepository,
    IWarehouseRepository warehouseRepository
) : ICreateWarehouseTransfer
{
    private readonly IWarehouseTransferRepository _warehouseTransferRepository = warehouseTransferRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;

    public async Task<CreateWarehouseTransferOutput> Execute(CreateWarehouseTransferInput input)
    {
        var product = await _productRepository.GetById(input.ProductId)
            ?? throw new NotFoundException($"Product id {input.ProductId} not found.");
        var sourceWarehouse = await _warehouseRepository.GetById(input.SourceWarehouseId)
            ?? throw new NotFoundException($"Warehouse id {input.SourceWarehouseId} not found.");
        var targetWarehouse = await _warehouseRepository.GetById(input.TargetWarehouseId)
            ?? throw new NotFoundException($"Warehouse id {input.TargetWarehouseId} not found.");

        var warehouseTransfer = input.GetEntity();
        var warehouseTransferId = await _warehouseTransferRepository.Create(warehouseTransfer);
        return new() { WarehouseTransferId = warehouseTransferId };
    }
}