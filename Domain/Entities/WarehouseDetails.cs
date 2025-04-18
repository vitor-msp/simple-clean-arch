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

    public static WarehouseDetails Rebuild(Guid id, DateTime createdAt, string city)
        => new(id, createdAt)
        {
            City = city,
        };
}