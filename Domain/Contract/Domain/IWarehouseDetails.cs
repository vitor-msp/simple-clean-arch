namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseDetails : ICloneable
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string? City { get; set; }
    public IWarehouse? Warehouse { get; set; }
}