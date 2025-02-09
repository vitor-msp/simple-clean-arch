using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductVariant: ICloneable
{
    // self-generated fields
    public long Id { get; }
    public string Sku { get; }
    public DateTime CreatedAt { get; }

    // required fields
    public IProduct Product { get; init; }
    public Color Color { get; init; }
    public Size Size { get; init; }
}