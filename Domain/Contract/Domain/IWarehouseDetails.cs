namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseDetails : ICloneable
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public IWarehouse Warehouse { get; }
    public string? City { get; set; }
}

public record WarehouseDetailsDto(Guid? Id = null, DateTime? CreatedAt = null, string? City = null);
