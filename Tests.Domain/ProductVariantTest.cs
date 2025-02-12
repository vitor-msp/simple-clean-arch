namespace SimpleCleanArch.Tests;

public class ProductVariantTest
{
    private readonly IProduct _product = new Product() { Name = "notebook", Price = 99.90 };
    private readonly Color _color = Color.Blue;
    private readonly Size _size = Size.Medium;

    [Fact]
    public void CreateProductVariant_Success()
    {
        var productVariant = new ProductVariant()
        {
            Product = _product,
            Color = _color,
            Size = _size,
        };
        Assert.IsType<Guid>(productVariant.Id);
        Assert.Equal("notebook-blue-medium", productVariant.Sku);
        Assert.IsType<DateTime>(productVariant.CreatedAt);
    }

    [Fact]
    public void RebuildProductVariant_Sucess()
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.Now;
        var productVariant = ProductVariant.Rebuild(
            id: id,
            createdAt: createdAt,
            color: _color,
            size: _size
        );
        productVariant.Product = _product;
        Assert.Equal(id, productVariant.Id);
        Assert.Equal("notebook-blue-medium", productVariant.Sku);
        Assert.Equal(createdAt, productVariant.CreatedAt);
        Assert.Equal(_color, productVariant.Color);
        Assert.Equal(_size, productVariant.Size);
    }
}