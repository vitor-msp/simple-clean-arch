using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("warehouses")]
public class WarehouseSchema : BaseSchema, IUpdatableSchema<IWarehouse>, IRegenerableSchema<IWarehouse>
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = "";

    [Column("description")]
    public string? Description { get; set; }

    public WarehouseDetailsSchema? Details { get; set; }

    public List<ProductSchema> Products { get; set; } = [];

    public WarehouseSchema() { }

    public WarehouseSchema(IWarehouse warehouse)
    {
        Hydrate(warehouse);
        Details = new WarehouseDetailsSchema(warehouse.Details) { Warehouse = this };
    }

    public void Update(IWarehouse warehouse)
    {
        Hydrate(warehouse);
        Details?.Update(warehouse.Details);
        base.Update();
    }

    public IWarehouse GetEntity()
        => Warehouse.Rebuild(
            id: Id,
            name: Name,
            description: Description,
            details: Details?.GetEntity()
                ?? throw new Exception("WarehouseSchema must contain Details to rebuild Warehouse."));

    private void Hydrate(IWarehouse warehouse)
    {
        Id = warehouse.Id;
        Name = warehouse.Name;
        Description = warehouse.Description;
    }
}