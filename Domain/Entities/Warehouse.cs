using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Warehouse : IWarehouse
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required string Name { get; init; }
    public string? Description { get; set; }

    private readonly IWarehouseDetails _details;
    public IWarehouseDetails Details
    {
        get => (IWarehouseDetails)_details.Clone();
    }

    public Warehouse()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        _details = new WarehouseDetails(this);
    }

    private Warehouse(Guid id, DateTime createdAt, IWarehouseDetails details)
    {
        Id = id;
        CreatedAt = createdAt;
        _details = details;
        _details.Warehouse = this;
    }

    public static IWarehouse Rebuild(Guid id, DateTime createdAt, string name, string? description, IWarehouseDetails details)
        => new Warehouse(id, createdAt, details)
        {
            Name = name,
            Description = description,
        };

    public void UpdateDetails(IWarehouseDetails details)
    {
        _details.City = details.City;
    }
}