using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Repository.Schema;

public class WarehouseDetailsSchema : BaseSchema<IWarehouseDetails, WarehouseDetailsDto>
{
    public string? City { get; set; }

    public WarehouseDetailsSchema() { }

    public WarehouseDetailsSchema(IWarehouseDetails details)
    {
        Hydrate(details);
    }

    public override void Update(IWarehouseDetails details)
    {
        Hydrate(details);
    }

    public override WarehouseDetailsDto GetEntity() => new(Id: Id, CreatedAt: CreatedAt, City: City);

    private void Hydrate(IWarehouseDetails details)
    {
        Id = details.Id;
        CreatedAt = details.CreatedAt;
        City = details.City;
    }
}