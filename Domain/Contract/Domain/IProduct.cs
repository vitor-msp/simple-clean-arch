namespace SimpleCleanArch.Domain.Contract;

public interface IProduct
{
    // self-generated fields
    public long Id { get; }
    public DateTime CreatedAt { get; }

    // required fields
    public string Name { get; init; }
    public double Price { get; set; }

    // optional fields
    public string? Description { get; set; }
    public string? Category { get; set; }
}