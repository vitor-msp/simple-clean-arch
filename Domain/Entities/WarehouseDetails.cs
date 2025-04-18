using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseDetails : IWarehouseDetails
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required string City { get; init; }

    public WarehouseDetails()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private WarehouseDetails(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static WarehouseDetails Rebuild(Guid id, DateTime createdAt, string city)
        => new(id, createdAt)
        {
            City = city,
        };
}