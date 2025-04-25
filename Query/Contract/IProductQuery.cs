using SimpleCleanArch.Repository.Schema;

namespace Query.Contract;

public interface IProductQuery
{
    public Task<ProductSchema?> GetById(int id);
}
