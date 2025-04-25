using SimpleCleanArch.Repository.Schema;

namespace Query.Contract;

public interface IProductQuery
{
    public Task<ProductSchema?> GetById(int id);
    public Task<List<ProductSchema>> GetMany(GetProductsInput input);
}

public class GetProductsInput(int? skip, int? limit, string? orderBy, bool? orderAsc)
{
    public required double? MinPrice { get; init; }
    public required double? MaxPrice { get; init; }
    public required string? Category { get; init; } = "";
    public int Skip { get; init; } = skip ?? 0;
    public int Limit { get; init; } = limit ?? 2;
    public string OrderBy { get; init; } = orderBy ?? "createdat";
    public bool OrderAsc { get; init; } = orderAsc ?? true;
}