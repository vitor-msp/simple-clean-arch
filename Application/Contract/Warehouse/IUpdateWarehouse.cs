using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.Contract;

public interface IUpdateWarehouse
{
    Task Execute(int id, UpdateWarehouseInput input);
}

public class UpdateWarehouseInput : IInputToUpdate<IWarehouse>
{
    [MaxLength(100)]
    public string? Description { get; set; }

    public required WarehouseDetails Details { get; init; }

    public class WarehouseDetails
    {
        [MaxLength(30)]
        public string? City { get; set; }
    }

    public void Update(IWarehouse warehouse)
    {
        warehouse.Description = Description;
        warehouse.UpdateDetails(new WarehouseDetailsDto(City: Details.City));
    }
}