using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Warehouse : IWarehouse
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required string Name { get; init; }
    public string? Description { get; set; }

    public Warehouse()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
}