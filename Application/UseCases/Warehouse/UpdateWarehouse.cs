using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class UpdateWarehouse(IWarehouseRepository repository) : IUpdateWarehouse
{
    private readonly IWarehouseRepository _repository = repository;

    public async Task Execute(int id, UpdateWarehouseInput input)
    {
        var warehouse = await _repository.GetById(id)
            ?? throw new NotFoundException($"Warehouse id {id} not found.");
        input.Update(warehouse);
        await _repository.Update(warehouse);
        await _repository.Commit();
    }
}