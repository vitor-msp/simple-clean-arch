using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Contract;

public interface ICreateWarehouse
{
    Task<CreateWarehouseOutput> Execute(CreateWarehouseInput input);
}

public class CreateWarehouseInput
{
    [Required(ErrorMessage = "name is required")]
    [MaxLength(20)]
    public string Name { get; set; } = "";

    [MaxLength(100)]
    public string? Description { get; set; }

    public required WarehouseDetails Details { get; init; }

    public class WarehouseDetails
    {
        [MaxLength(30)]
        public string? City { get; set; }
    }

    public IWarehouse GetEntity()
    {
        var warehouse = new Warehouse()
        {
            Name = Name,
            Description = Description,
        };
        warehouse.UpdateDetails(new WarehouseDetailsDto(City: Details.City));
        return warehouse;
    }
}

public class CreateWarehouseOutput
{
    public int WarehouseId { get; set; }
}