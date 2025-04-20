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
        base.Update();
    }

    public IWarehouse GetEntity()
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