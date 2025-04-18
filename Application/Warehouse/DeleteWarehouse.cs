using System.Text.Json;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class DeleteWarehouse(IWarehouseRepository repository) : IDeleteWarehouse
{
    private readonly IWarehouseRepository _repository = repository;

    public async Task Execute(Guid id)
    {
        var warehouse = await _repository.GetById(id)
            ?? throw new Exception($"Warehouse id {id} not found.");
        await _repository.Delete(warehouse);
        await _repository.Commit();
    }
}