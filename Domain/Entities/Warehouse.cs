using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Warehouse : IWarehouse
{
    public int Id { get; }
    public required string Name { get; init; }
    public string? Description { get; set; }

    private readonly IWarehouseDetails _details;
    public IWarehouseDetails Details
    {
        get => (IWarehouseDetails)_details.Clone();
    }

    public Warehouse()
    {
        Id = default;
        _details = new WarehouseDetails() { Warehouse = this };
    }

    private Warehouse(int id, WarehouseDetailsDto details)
    {
        Id = id;
        _details = WarehouseDetails.Rebuild(
            details: details,
            warehouse: this
        );
    }

    public static IWarehouse Rebuild(int id, string name, string? description, WarehouseDetailsDto details)
        => new Warehouse(id, details)
        {
            Name = name,
            Description = description,
        };

    public void UpdateDetails(WarehouseDetailsDto details)
    {
        _details.City = details.City;
    }
}