using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Repository.Schema;

[Table("warehouse_details")]
public class WarehouseDetailsSchema : BaseSchema<IWarehouseDetails, WarehouseDetailsDto>
{
    [Key, ForeignKey("Warehouse"), Column("warehouse_id")]
    public int WarehouseId { get; set; }

    public WarehouseSchema Warehouse { get; set; }

    [Column("city")]
    public string? City { get; set; }

    public WarehouseDetailsSchema() { }

    public WarehouseDetailsSchema(IWarehouseDetails details)
    {
        Hydrate(details);
    }

    public override void Update(IWarehouseDetails details)
    {
        Hydrate(details);
        base.Update(details);
    }

    public override WarehouseDetailsDto GetEntity() => new(CreatedAt: CreatedAt, City: City);

    private void Hydrate(IWarehouseDetails details)
    {
        CreatedAt = details.CreatedAt;
        City = details.City;
    }
}