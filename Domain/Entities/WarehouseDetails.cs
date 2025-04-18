using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseDetails : IWarehouseDetails
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required IWarehouse Warehouse { get; init; }
    public string? City { get; set; }

    public WarehouseDetails()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private WarehouseDetails(Guid? id, DateTime? createdAt)
    {
        Id = id ?? throw new Exception("Cannot rebuild WarehouseDetails without Id."); ;
        CreatedAt = createdAt ?? throw new Exception("Cannot rebuild WarehouseDetails without CreatedAt."); ;
    }

    public static IWarehouseDetails Rebuild(WarehouseDetailsDto details, IWarehouse warehouse)
        => new WarehouseDetails(details.Id, details.CreatedAt)
        {
            City = details.City,
            Warehouse = warehouse,
        };

    public object Clone()
    {
        var details = Rebuild(
            details: new WarehouseDetailsDto(Id: Id, CreatedAt: CreatedAt, City: City),
            warehouse: Warehouse);
        return details;
    }
}