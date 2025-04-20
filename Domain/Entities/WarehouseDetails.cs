using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseDetails : IWarehouseDetails
{
    public required IWarehouse Warehouse { get; init; }
    public string? City { get; set; }

    public WarehouseDetails() { }

    public static IWarehouseDetails Rebuild(WarehouseDetailsDto details, IWarehouse warehouse)
        => new WarehouseDetails()
        {
            City = details.City,
            Warehouse = warehouse,
        };

    public object Clone()
        => Rebuild(
            details: new WarehouseDetailsDto(City: City),
            warehouse: Warehouse
        );
}