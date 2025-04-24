namespace SimpleCleanArch.Domain.ValueObjects;

public class Sku
{
    public required string ProductName { get; init; }
    public required Color Color { get; init; }
    public required Size Size { get; init; }
    public string Value
    {
        get
        {
            var name = FormatSkuText(ProductName);
            var color = FormatSkuText(Color.ToString());
            var size = FormatSkuText(Size.ToString());
            return $"{name}-{color}-{size}";
        }
    }

    private static string FormatSkuText(string text)
        => text.ToLower().Replace(" ", "_");
}