using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.Contract;

public interface IUpdateWarehouse
{
    Task Execute(Guid id, UpdateWarehouseInput input);
}

public class UpdateWarehouseInput
{
    [MaxLength(100)]
    public string? Description { get; set; }

    public required WarehouseDetails Details { get; init; }

    public class WarehouseDetails
    {
        [MaxLength(30)]
        public string? City { get; set; }
    }

    public IWarehouse Update(IWarehouse warehouse)
    {
        warehouse.Description = Description;
        warehouse.UpdateDetails(new Domain.Entities.WarehouseDetails()
        {
            City = Details.City,
        });
        return warehouse;
    }
}