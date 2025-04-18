namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseDetails
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string City { get; init; }
}