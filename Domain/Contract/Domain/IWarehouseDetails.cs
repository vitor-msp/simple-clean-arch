namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseDetails : ICloneable
{
    public DateTime CreatedAt { get; }
    public IWarehouse Warehouse { get; }
    public string? City { get; set; }
}

public record WarehouseDetailsDto(DateTime? CreatedAt = null, string? City = null);
