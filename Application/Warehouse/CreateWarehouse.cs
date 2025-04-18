using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class CreateWarehouse(IWarehouseRepository repository) : ICreateWarehouse
{
    private readonly IWarehouseRepository _repository = repository;

    public async Task<CreateWarehouseOutput> Execute(CreateWarehouseInput input)
    {
        var savedWarehouse = await _repository.GetByName(input.Name);
        var warehouse = input.GetEntity();
        if (savedWarehouse is not null)
            throw new ConflictException($"Warehouse with name {warehouse.Name} already exists.");
        await _repository.Create(warehouse);
        await _repository.Commit();
        return new() { WarehouseId = warehouse.Id };
    }
}