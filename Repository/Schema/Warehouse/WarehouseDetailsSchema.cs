using SimpleCleanArch.Domain.Contract;

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

    public WarehouseDetailsDto GetEntity() => new(Id: Id, CreatedAt: CreatedAt, City: City);

    private void Hydrate(IWarehouseDetails details)
    {
        Id = details.Id;
        CreatedAt = details.CreatedAt;
        City = details.City;
    }
}