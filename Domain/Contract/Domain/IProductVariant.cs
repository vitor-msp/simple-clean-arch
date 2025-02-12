using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductVariant : ICloneable
{
    public long Id { get; }
    public DateTime CreatedAt { get; }
    public Color Color { get; init; }
    public Size Size { get; init; }
    public IProduct? Product { get; set; }
    public string Sku { get; }
}