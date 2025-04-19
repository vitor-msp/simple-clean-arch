namespace SimpleCleanArch.Tests.Domain;

public class ProductTest
{
    private readonly DateTime _createdAt = DateTime.Now;

    private Product GetProduct(List<ProductVariantDto>? variants) => Product.Rebuild(
        id: 1,
        createdAt: _createdAt,
        name: "my product",
        price: 10.6,
        description: "my product description",
        category: "category",
        variants: variants ?? []
    );

    private Product GetProductWithVariant()
        => GetProduct([new ProductVariantDto(
                Id: 1,
                CreatedAt: _createdAt,
                Color: Color.Blue,
                Size: Size.Medium,
                Sku: "my_product-blue-medium"
            )]);

    private Product GetProductWithTwoVariants()
    {
        var variant1 = new ProductVariantDto(
            Id: 1,
            CreatedAt: _createdAt,
            Color: Color.Red,
            Size: Size.Small,
            Sku: "my_product-red-small"
        );
        var variant2 = new ProductVariantDto(
            Id: 2,
            CreatedAt: _createdAt,
            Color: Color.Green,
            Size: Size.Medium,
            Sku: "my_product-green-medium"
        );
        return GetProduct([variant1, variant2]);
    }

    [Fact]
    public void CreateProduct_Success()
    {
        var product = new Product()
        {
            Name = "name",
            Price = 16.2,
            Description = "description",
            Category = "category"
        };
        Assert.IsType<int>(product.Id);
        Assert.IsType<DateTime>(product.CreatedAt);
    }

    [Fact]
    public void CreateProduct_InvalidPrice()
    {
        var invalidPrice = -2.75;
        Action action = () => new Product()
        {
            Name = "product",
            Price = invalidPrice,
            Description = "description",
            Category = "category"
        };
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RebuildProduct_Success()
    {
        var createdAt = DateTime.Now;
        var product = Product.Rebuild
        (
            id: 1,
            createdAt: createdAt,
            name: "product",
            price: 15.1,
            description: "description",
            category: "category",
            []
        );
        Assert.Equal(1, product.Id);
        Assert.Equal(createdAt, product.CreatedAt);
        Assert.Equal("product", product.Name);
        Assert.Equal(15.1, product.Price);
        Assert.Equal("description", product.Description);
        Assert.Equal("category", product.Category);
        Assert.Equal([], product.ProductVariants);
    }

    [Fact]
    public void GetProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-medium";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.Equal(1, variant.Id);
        Assert.Equal(_createdAt, variant.CreatedAt);
        Assert.Equal(Color.Blue, variant.Color);
        Assert.Equal(Size.Medium, variant.Size);
        Assert.Equal(product, variant.Product);
        Assert.Equal(sku, variant.Sku);
    }

    [Fact]
    public void AddProductVariant_Success()
    {
        var product = GetProductWithVariant();
        product.AddProductVariant(new ProductVariantDto(
            Color: Color.Red,
            Size: Size.Small
        ));
        var sku = "my_product-red-small";
        var variant = product.GetProductVariant(sku);
        Assert.NotNull(variant);
        Assert.IsType<int>(variant.Id);
        Assert.IsType<DateTime>(variant.CreatedAt);
        Assert.Equal(Color.Red, variant.Color);
        Assert.Equal(Size.Small, variant.Size);
        Assert.Equal(product, variant.Product);
        Assert.Equal(sku, variant.Sku);
    }

    [Fact]
    public void ListProductVariants_Success()
    {
        var product = GetProductWithVariant();
        product.AddProductVariant(new ProductVariantDto(
            Color: Color.Red,
            Size: Size.Small
        ));
        Assert.Equal(2, product.ProductVariants.Count);
    }

    [Fact]
    public void RemoveProductVariant_Success()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-blue-medium";
        product.RemoveProductVariant(sku);
        var variants = product.ProductVariants;
        Assert.Empty(variants);
    }

    [Fact]
    public void RemoveProductVariant_SkuNotFound()
    {
        var product = GetProductWithVariant();
        var sku = "my_product-green-large";
        Action action = () => product.RemoveProductVariant(sku);
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void UpdateProductVariant_Success()
    {
        var product = GetProductWithTwoVariants();
        var newVariants = new List<ProductVariantDto>
        {
            new ProductVariantDto(
                Color: Color.Red,
                Size: Size.Small,
                Description : "new description",
                Sku: "my_product-red-small"
            ),
            new ProductVariantDto(
                Color: Color.Blue,
                Size: Size.Large,
                Sku: "my_product-blue-large"
            )
        };
        product.UpdateProductVariants(newVariants);
        Assert.Equal(2, product.ProductVariants.Count);
        var variantGreenMedium = product.ProductVariants.Find(variant => variant.Sku == "my_product-green-medium");
        Assert.Null(variantGreenMedium);
        var variantRedSmall = product.ProductVariants.Find(variant => variant.Sku == "my_product-red-small");
        Assert.NotNull(variantRedSmall);
        Assert.Equal("new description", variantRedSmall.Description);
        var variantBlueLarge = product.ProductVariants.Find(variant => variant.Sku == "my_product-blue-large");
        Assert.NotNull(variantBlueLarge);
        Assert.Null(variantBlueLarge.Description);
    }
}