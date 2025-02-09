using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.Contract;

public interface IGetProduct
{
    Task<GetProductOutput> Execute(long id);
}

public class GetProductOutput
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }

    public static GetProductOutput FromEntity(IProduct product)
        => new()
        {
            Id = product.Id,
            CreatedAt = product.CreatedAt,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
        };
}