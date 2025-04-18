using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Warehouse : IWarehouse
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required string Name { get; init; }
    public string? Description { get; set; }
    public IWarehouseDetails Details { get; }

    public Warehouse()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Details = new WarehouseDetails(this);
    }

    private Warehouse(Guid id, DateTime createdAt, WarehouseDetails details)
    {
        Id = id;
        CreatedAt = createdAt;
        Details = details;
        details.Warehouse = this;
    }

    public static Warehouse Rebuild(Guid id, DateTime createdAt, string name, string? description, WarehouseDetails details)
        => new(id, createdAt, details)
        {
            Name = name,
            Description = description,
        };
}