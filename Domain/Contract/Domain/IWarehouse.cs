namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouse
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string Name { get; init; }
    public string? Description { get; set; }
    public IWarehouseDetails Details { get; }

    public void UpdateDetails(WarehouseDetailsDto details);
}