using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class CreateWarehouse(IWarehouseRepository repository) : ICreateWarehouse
{
    private readonly IWarehouseRepository _repository = repository;

    public async Task<CreateWarehouseOutput> Execute(CreateWarehouseInput input)
    {
        var savedWarehouse = await _repository.GetByName(input.Name);
        if (savedWarehouse is not null)
            throw new ConflictException($"Warehouse with name {input.Name} already exists.");
        var warehouse = input.GetEntity();
        var warehouseId = await _repository.Create(warehouse);
        return new() { WarehouseId = warehouseId };
    }
}