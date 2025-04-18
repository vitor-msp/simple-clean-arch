using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class WarehouseDetailsSchema
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? City { get; set; }

    public WarehouseDetailsSchema() { }

    public WarehouseDetailsSchema(IWarehouseDetails details)
    {
        Hydrate(details);
    }

    public void Update(IWarehouseDetails details)
    {
        Hydrate(details);
    }

    public IWarehouseDetails GetEntity()
        => WarehouseDetails.Rebuild(id: Id, createdAt: CreatedAt, city: City);

    private void Hydrate(IWarehouseDetails details)
    {
        Id = details.Id;
        CreatedAt = details.CreatedAt;
        City = details.City;
    }
}