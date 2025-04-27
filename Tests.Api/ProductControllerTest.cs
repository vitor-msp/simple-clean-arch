namespace SimpleCleanArch.Tests.Api;

[Collection("Tests.Api")]
public class ProductControllerTest : BaseControllerTest
{
    private readonly ProductController _controller;
    private readonly AppDbContext _context;

    public ProductControllerTest() : base()
    {
        (_controller, _context) = MakeSut();
    }

    private (ProductController _controller, AppDbContext _context) MakeSut()
    {
        var _context = CreateContext();
        var repository = new ProductRepositorySqlite(_context);
        var mail = new Mock<IMailGateway>().Object;
        var createProduct = new CreateProduct(repository, mail);
        var deleteProduct = new DeleteProduct(repository, mail);
        var updateProduct = new UpdateProduct(repository);
        var productQuery = new Mock<IProductQuery>().Object;
        var _controller = new ProductController(createProduct, deleteProduct, updateProduct, productQuery);
        return (_controller, _context);
    }

    protected override async Task CleanDatabase()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM products;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM product_variants;");
    }

    [Fact]
    public async Task PostProduct_Success()
    {
        var variants = new List<CreateProductInput.ProductVariant>()
        {
            new() {
                Color = Color.Blue.ToString(),
                Size = Size.Large.ToString(),
            },
            new() {
                Color = Color.Green.ToString(),
                Size = Size.Medium.ToString(),
            },
        };
        var input = new CreateProductInput()
        {
            Name = "my product",
            Price = 10.69,
            Description = "my product description",
            Category = "category",
            ProductVariants = variants,
        };
        var output = await _controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateProductOutput>(outputResult.Value);
        var productId = Assert.IsType<int>(outputContent.ProductId);
        var productSchema = await _context.Products.FindAsync(productId);
        Assert.NotNull(productSchema);
        Assert.IsType<DateTime>(productSchema.CreatedAt);
        Assert.Equal("my product", productSchema.Name);
        Assert.Equal(10.69, productSchema.Price);
        Assert.Equal("my product description", productSchema.Description);
        Assert.Equal("category", productSchema.Category);
        Assert.Equal(2, productSchema.ProductVariants.Count);
        Assert.Equal(default, productSchema.UpdatedAt);
        var variantBlueLarge = productSchema.ProductVariants.Find(v => v.Sku == "my_product-blue-large");
        Assert.NotNull(variantBlueLarge);
        Assert.Equal(Color.Blue.ToString(), variantBlueLarge.Color);
        Assert.Equal(Size.Large.ToString(), variantBlueLarge.Size);
        Assert.Equal(default, variantBlueLarge.UpdatedAt);
        var variantGreenMedium = productSchema.ProductVariants.Find(v => v.Sku == "my_product-green-medium");
        Assert.NotNull(variantGreenMedium);
        Assert.Equal(Color.Green.ToString(), variantGreenMedium.Color);
        Assert.Equal(Size.Medium.ToString(), variantGreenMedium.Size);
        Assert.Equal(default, variantGreenMedium.UpdatedAt);
    }

    [Fact]
    public async Task PostProduct_InvalidPrice()
    {
        var input = new CreateProductInput()
        {
            Name = "invalid-product",
            Price = -10,
            Description = "description",
            Category = "category",
        };
        var output = await _controller.Post(input);
        Assert.IsType<UnprocessableEntityObjectResult>(output.Result);
        var productsSchema = await _context.Products.FirstOrDefaultAsync(product => product.Name.Equals("invalid-product"));
        Assert.Null(productsSchema);
    }

    [Fact]
    public async Task PatchProduct_Success()
    {
        var productSchema = new ProductSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my product",
            Price = 10.56,
            Description = "my product description",
            Category = "category",
        };
        var variant1 = new ProductVariantSchema()
        {
            Color = Color.Red.ToString(),
            Size = Size.Small.ToString(),
            Product = productSchema,
            Description = "red small description",
            Sku = "my_product-red-small",
        };
        var variant2 = new ProductVariantSchema()
        {
            Color = Color.Green.ToString(),
            Size = Size.Medium.ToString(),
            Product = productSchema,
            Description = "green medium description",
            Sku = "my_product-green-medium",
        };
        productSchema.ProductVariants = [variant1, variant2];
        await _context.Products.AddAsync(productSchema);
        await _context.SaveChangesAsync();
        var input = new UpdateProductInput()
        {
            Price = 15.74,
            Description = "new description",
            Category = "new category",
            ProductVariants = [
                new UpdateProductInput.ProductVariant()
                {
                    Color = Color.Green.ToString(),
                    Size = Size.Medium.ToString(),
                    Description = "green medium new description",
                    Sku = "my_product-green-medium",
                },
                new UpdateProductInput.ProductVariant()
                {
                    Color = Color.Blue.ToString(),
                    Size = Size.Large.ToString(),
                    Description = "blue large description",
                },
            ]
        };
        var output = await _controller.Patch(productSchema.Id, input);
        Assert.IsType<NoContentResult>(output);
        Assert.Equal("my product", productSchema.Name);
        Assert.Equal(15.74, productSchema.Price);
        Assert.Equal("new description", productSchema.Description);
        Assert.Equal("new category", productSchema.Category);
        Assert.Equal(2, productSchema.ProductVariants.Count);
        Assert.NotEqual(default, productSchema.UpdatedAt);
        var variantRedSmall = productSchema.ProductVariants.Find(v => v.Sku == "my_product-red-small");
        Assert.Null(variantRedSmall);
        var variantGreenMedium = productSchema.ProductVariants.Find(v => v.Sku == "my_product-green-medium");
        Assert.NotNull(variantGreenMedium);
        Assert.Equal(Color.Green.ToString(), variantGreenMedium.Color);
        Assert.Equal(Size.Medium.ToString(), variantGreenMedium.Size);
        Assert.Equal("green medium new description", variantGreenMedium.Description);
        Assert.NotEqual(default, variantGreenMedium.UpdatedAt);
        var variantBlueLarge = productSchema.ProductVariants.Find(v => v.Sku == "my_product-blue-large");
        Assert.NotNull(variantBlueLarge);
        Assert.Equal(Color.Blue.ToString(), variantBlueLarge.Color);
        Assert.Equal(Size.Large.ToString(), variantBlueLarge.Size);
        Assert.Equal("blue large description", variantBlueLarge.Description);
        Assert.Equal(default, variantBlueLarge.UpdatedAt);
    }

    [Fact]
    public async Task DeleteProduct_Success()
    {
        var productSchema = new ProductSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my product",
            Price = 10.56,
            Description = "my product description",
            Category = "category",
        };
        var variant = new ProductVariantSchema()
        {
            Color = Color.Red.ToString(),
            Size = Size.Small.ToString(),
            Product = productSchema,
            Description = "red small description",
            Sku = "my_product-red-small",
        };
        productSchema.ProductVariants = [variant];
        await _context.Products.AddAsync(productSchema);
        await _context.SaveChangesAsync();
        var output = await _controller.Delete(productSchema.Id);
        Assert.IsType<NoContentResult>(output);
        var deletedProductSchema = await _context.Products.FindAsync(productSchema.Id);
        Assert.Null(deletedProductSchema);
    }
}