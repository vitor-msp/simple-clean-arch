namespace SimpleCleanArch.Tests.Domain;

public class SkuTest
{
    [Fact]
    public void CreateSku_Success()
    {
        var sku = new Sku()
        {
            ProductName = "my product",
            Color = Color.Blue,
            Size = Size.Large,
        };
        Assert.Equal("my_product-blue-large", sku.Value);
    }
}