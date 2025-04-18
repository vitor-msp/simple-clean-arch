namespace SimpleCleanArch.Tests.Domain;

public class ProductVariantTest
{
    [Fact]
    public void CreateProductVariant_Success()
    {
        var productVariant = new ProductVariant()
        {
            Product = new Product() { Name = "notebook", Price = 99.90 },
            Color = Color.Blue,
            Size = Size.Medium,
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
            color: Color.Blue,
            size: Size.Medium,
            description: null
        );
        productVariant.Product = new Product() { Name = "notebook", Price = 99.90 };
        Assert.Equal(id, productVariant.Id);
        Assert.Equal("notebook-blue-medium", productVariant.Sku);
        Assert.Equal(createdAt, productVariant.CreatedAt);
        Assert.Equal(Color.Blue, productVariant.Color);
        Assert.Equal(Size.Medium, productVariant.Size);
    }
}