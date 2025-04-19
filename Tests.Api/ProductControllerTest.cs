namespace SimpleCleanArch.Tests.Api;

public class ProductControllerTest
{
    private static (ProductController controller, AppDbContext context) MakeSut()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
        var context = new AppDbContext(contextOptions);
        context.Database.EnsureCreatedAsync();
        var repository = new ProductRepositorySqlite(context);
        var mail = new Mock<IMailGateway>().Object;
        var createProduct = new CreateProduct(repository, mail);
        var deleteProduct = new DeleteProduct(repository, mail);
        var updateProduct = new UpdateProduct(repository);
        var controller = new ProductController(createProduct, deleteProduct, updateProduct);
        return (controller, context);
    }

    [Fact]
    public async Task PostProduct_Success()
    {
        var variants = new List<CreateProductInput.ProductVariant>()
        {
            new() {
                Color = Color.Blue,
                Size = Size.Large,
            },
            new() {
                Color = Color.Green,
                Size = Size.Medium,
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
        var (controller, context) = MakeSut();
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateProductOutput>(outputResult.Value);
        var productId = Assert.IsType<Guid>(outputContent.ProductId);
        var productSchema = await context.Products.FindAsync(productId);
        Assert.NotNull(productSchema);
        Assert.IsType<DateTime>(productSchema.CreatedAt);
        Assert.Equal("my product", productSchema.Name);
        Assert.Equal(10.69, productSchema.Price);
        Assert.Equal("my product description", productSchema.Description);
        Assert.Equal("category", productSchema.Category);
        Assert.Equal(2, productSchema.ProductVariants.Count);
        var variantBlueLarge = productSchema.ProductVariants.Find(v => v.Sku == "my_product-blue-large");
        Assert.NotNull(variantBlueLarge);
        Assert.Equal(Color.Blue, variantBlueLarge.Color);
        Assert.Equal(Size.Large, variantBlueLarge.Size);
        var variantGreenMedium = productSchema.ProductVariants.Find(v => v.Sku == "my_product-green-medium");
        Assert.NotNull(variantGreenMedium);
        Assert.Equal(Color.Green, variantGreenMedium.Color);
        Assert.Equal(Size.Medium, variantGreenMedium.Size);
    }

    [Fact]
    public async Task PostProduct_InvalidPrice()
    {
        var input = new CreateProductInput()
        {
            Name = "product",
            Price = -10,
            Description = "description",
            Category = "category",
        };
        var (controller, context) = MakeSut();
        var output = await controller.Post(input);
        Assert.IsType<UnprocessableEntityObjectResult>(output.Result);
        var productsSchema = await context.Products.ToListAsync();
        Assert.Empty(productsSchema);
    }

    [Fact]
    public async Task PatchProduct_Success()
    {
        var productSchema = new ProductSchema()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Name = "my product",
            Price = 10.56,
            Description = "my product description",
            Category = "category",
        };
        var variant1 = new ProductVariantSchema()
        {
            Color = Color.Red,
            Size = Size.Small,
            Product = productSchema,
            Description = "red small description",
            Sku = "my_product-red-small",
        };
        var variant2 = new ProductVariantSchema()
        {
            Color = Color.Green,
            Size = Size.Medium,
            Product = productSchema,
            Description = "green medium description",
            Sku = "my_product-green-medium",
        };
        productSchema.ProductVariants = [variant1, variant2];
        var (controller, context) = MakeSut();
        await context.Products.AddAsync(productSchema);
        var input = new UpdateProductInput()
        {
            Price = 15.74,
            Description = "new description",
            Category = "new category",
            ProductVariants = [
                new UpdateProductInput.ProductVariant()
                {
                    Color = Color.Green,
                    Size = Size.Medium,
                    Description = "green medium new description",
                    Sku = "my_product-green-medium",
                },
                new UpdateProductInput.ProductVariant()
                {
                    Color = Color.Blue,
                    Size = Size.Large,
                    Description = "blue large description",
                },
            ]
        };
        var output = await controller.Patch(productSchema.Id, input);
        Assert.IsType<NoContentResult>(output);
        Assert.Equal("my product", productSchema.Name);
        Assert.Equal(15.74, productSchema.Price);
        Assert.Equal("new description", productSchema.Description);
        Assert.Equal("new category", productSchema.Category);
        Assert.Equal(2, productSchema.ProductVariants.Count);
        var variantRedSmall = productSchema.ProductVariants.Find(v => v.Sku == "my_product-red-small");
        Assert.Null(variantRedSmall);
        var variantGreenMedium = productSchema.ProductVariants.Find(v => v.Sku == "my_product-green-medium");
        Assert.NotNull(variantGreenMedium);
        Assert.Equal(Color.Green, variantGreenMedium.Color);
        Assert.Equal(Size.Medium, variantGreenMedium.Size);
        Assert.Equal("green medium new description", variantGreenMedium.Description);
        var variantBlueLarge = productSchema.ProductVariants.Find(v => v.Sku == "my_product-blue-large");
        Assert.NotNull(variantBlueLarge);
        Assert.Equal(Color.Blue, variantBlueLarge.Color);
        Assert.Equal(Size.Large, variantBlueLarge.Size);
        Assert.Equal("blue large description", variantBlueLarge.Description);
    }

    [Fact]
    public async Task DeleteProduct_Success()
    {
        var productId = Guid.NewGuid();
        var productSchema = new ProductSchema()
        {
            Id = productId,
            CreatedAt = DateTime.Now,
            Name = "my product",
            Price = 10.56,
            Description = "my product description",
            Category = "category",
        };
        var variant = new ProductVariantSchema()
        {
            Color = Color.Red,
            Size = Size.Small,
            Product = productSchema,
            Description = "red small description",
            Sku = "my_product-red-small",
        };
        productSchema.ProductVariants = [variant];
        var (controller, context) = MakeSut();
        await context.Products.AddAsync(productSchema);
        var output = await controller.Delete(productId);
        Assert.IsType<NoContentResult>(output);
        var deletedProductSchema = await context.Products.FindAsync(productId);
        Assert.Null(deletedProductSchema);
    }
}