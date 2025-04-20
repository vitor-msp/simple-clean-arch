namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseDetails : ICloneable
{
    public IWarehouse Warehouse { get; }
    public string? City { get; set; }
}

public record WarehouseDetailsDto(string? City = null);
