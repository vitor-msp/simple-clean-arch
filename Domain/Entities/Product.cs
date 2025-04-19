using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Product : IProduct
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public required string Name { get; init; }
    private double _price;
    public required double Price
    {
        get { return _price; }
        set
        {
            if (value <= _minPrice || value > _maxPrice)
                throw new DomainException($"Price must be between {_minPrice} and {_maxPrice}.");
            _price = value;
        }
    }
    public string? Description { get; set; }
    public string? Category { get; set; }
    private static readonly double _minPrice = 0;
    private static readonly double _maxPrice = 100;

    private List<IProductVariant> _productVariants = [];
    public List<IProductVariant> ProductVariants
    {
        get
        {
            var newList = new List<IProductVariant>();
            _productVariants.ForEach(variant => newList.Add((IProductVariant)variant.Clone()));
            return newList;
        }
    }

    public Product()
    {
        Id = default;
        CreatedAt = DateTime.Now;
    }

    private Product(int id, DateTime createdAt, List<ProductVariantDto> variants)
    {
        Id = id;
        CreatedAt = createdAt;
        _productVariants = variants.Select(
            variant => ProductVariant.Rebuild(variant: variant, product: this))
        .ToList();
    }

    public static Product Rebuild(int id, DateTime createdAt, string name,
        double price, string? description, string? category, List<ProductVariantDto> variants)
        => new(id, createdAt, variants)
        {
            Name = name,
            Price = price,
            Description = description,
            Category = category,
        };

    public void AddProductVariant(ProductVariantDto variant)
    {
        _productVariants.Add(new ProductVariant()
        {
            Color = variant.Color ?? throw new Exception("Cannot create a product variant without Color."),
            Size = variant.Size ?? throw new Exception("Cannot create a product variant without Size."),
            Description = variant.Description,
            Product = this,
        }.GenerateSku());
    }

    public void RemoveProductVariant(string sku)
    {
        var index = _productVariants.FindIndex(variant => variant.Sku == sku);
        if (index == -1)
            throw new DomainException($"Product variant sku {sku} not found.");
        _productVariants.RemoveAt(index);
    }

    public IProductVariant? GetProductVariant(string sku)
    {
        var variant = _productVariants.Find(variant => variant.Sku == sku);
        if (variant is null) return null;
        return (IProductVariant)variant.Clone();
    }

    public void UpdateProductVariants(List<ProductVariantDto> newVariants)
    {
        _productVariants = EliminateDeletedProductVariants(newVariants);
        UpdateOrCreateProductVariants(newVariants);
    }

    private List<IProductVariant> EliminateDeletedProductVariants(List<ProductVariantDto> newVariants)
        => _productVariants.Where(variant
            => newVariants.Any(newVariant => newVariant.Sku == variant.Sku)).ToList();

    private void UpdateOrCreateProductVariants(List<ProductVariantDto> newVariants)
    {
        newVariants.ForEach(newVariant =>
        {
            var variant = _productVariants.Find(variant => variant.Sku == newVariant.Sku);
            if (variant is null)
                _productVariants.Add(new ProductVariant()
                {
                    Color = newVariant.Color ?? throw new Exception("Cannot create a product variant without Color."),
                    Size = newVariant.Size ?? throw new Exception("Cannot create a product variant without Size."),
                    Description = newVariant.Description,
                    Product = this,
                }.GenerateSku());
            else
                variant.Description = newVariant.Description;
        });
    }
}