using Query.Contract;

namespace SimpleCleanArch.Tests.Api;

public class ProductControllerTest : BaseControllerTest
{
    private async Task<(ProductController controller, AppDbContext context)> MakeSut()
    {
        var context = await CreateContext();
        var repository = new ProductRepositorySqlite(context);
        var mail = new Mock<IMailGateway>().Object;
        var createProduct = new CreateProduct(repository, mail);
        var deleteProduct = new DeleteProduct(repository, mail);
        var updateProduct = new UpdateProduct(repository);
        var productQuery = new Mock<IProductQuery>().Object;
        var controller = new ProductController(createProduct, deleteProduct, updateProduct, productQuery);
        return (controller, context);
    }

    [Fact]
    public async Task PostProduct_Success()
    {
        var (controller, context) = await MakeSut();
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
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateProductOutput>(outputResult.Value);
        var productId = Assert.IsType<int>(outputContent.ProductId);
        var productSchema = await context.Products.FindAsync(productId);
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
        var (controller, context) = await MakeSut();
        var input = new CreateProductInput()
        {
            Name = "invalid-product",
            Price = -10,
            Description = "description",
            Category = "category",
        };
        var output = await controller.Post(input);
        Assert.IsType<UnprocessableEntityObjectResult>(output.Result);
        var productsSchema = await context.Products.FirstOrDefaultAsync(product => product.Name.Equals("invalid-product"));
        Assert.Null(productsSchema);
    }

    [Fact]
    public async Task PatchProduct_Success()
    {
        var (controller, context) = await MakeSut();
        var productSchema = new ProductSchema()
        {
            CreatedAt = DateTime.Now,
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
        await context.Products.AddAsync(productSchema);
        await context.SaveChangesAsync();
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
        var output = await controller.Patch(productSchema.Id, input);
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
        var (controller, context) = await MakeSut();
        var productSchema = new ProductSchema()
        {
            CreatedAt = DateTime.Now,
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
        await context.Products.AddAsync(productSchema);
        await context.SaveChangesAsync();
        var output = await controller.Delete(productSchema.Id);
        Assert.IsType<NoContentResult>(output);
        var deletedProductSchema = await context.Products.FindAsync(productSchema.Id);
        Assert.Null(deletedProductSchema);
    }
}