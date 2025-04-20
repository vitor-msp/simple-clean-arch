using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseDetails : IWarehouseDetails
{
    public DateTime CreatedAt { get; }
    public required IWarehouse Warehouse { get; init; }
    public string? City { get; set; }

    public WarehouseDetails()
    {
        CreatedAt = DateTime.Now;
    }

    private WarehouseDetails(WarehouseDetailsDto details)
    {
        CreatedAt = details.CreatedAt ?? throw new Exception("Cannot rebuild WarehouseDetails without CreatedAt."); ;
    }

    public static IWarehouseDetails Rebuild(WarehouseDetailsDto details, IWarehouse warehouse)
        => new WarehouseDetails(details)
        {
            City = details.City,
            Warehouse = warehouse,
        };

    public object Clone()
        => Rebuild(
            details: new WarehouseDetailsDto(CreatedAt: CreatedAt, City: City),
            warehouse: Warehouse
        );
}