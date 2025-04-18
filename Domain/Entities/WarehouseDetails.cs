using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseDetails : IWarehouseDetails
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string? City { get; set; }
    public IWarehouse? Warehouse { get; set; }

    public WarehouseDetails()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public WarehouseDetails(Warehouse warehouse) : this()
    {
        Warehouse = warehouse;
    }

    private WarehouseDetails(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IWarehouseDetails Rebuild(Guid id, DateTime createdAt, string? city)
        => new WarehouseDetails(id, createdAt)
        {
            City = city,
        };

    public object Clone()
    {
        var details = Rebuild(Id, CreatedAt, City);
        details.Warehouse = Warehouse;
        return details;
    }
}