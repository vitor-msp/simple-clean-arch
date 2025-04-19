using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class WarehouseSchema : BaseSchema<IWarehouse>
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public WarehouseDetailsSchema Details { get; set; }

    public WarehouseSchema() { }

    public WarehouseSchema(IWarehouse warehouse)
    {
        Hydrate(warehouse);
        Details = new WarehouseDetailsSchema(warehouse.Details);
    }

    public void Update(IWarehouse warehouse)
    {
        Hydrate(warehouse);
        Details.Update(warehouse.Details);
    }

    public override IWarehouse GetEntity()
        => Warehouse.Rebuild(id: Id, createdAt: CreatedAt, name: Name,
            description: Description, details: Details.GetEntity());

    private void Hydrate(IWarehouse warehouse)
    {
        Id = warehouse.Id;
        CreatedAt = warehouse.CreatedAt;
        Name = warehouse.Name;
        Description = warehouse.Description;
    }
}