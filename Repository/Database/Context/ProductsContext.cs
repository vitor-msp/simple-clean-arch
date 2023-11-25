using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Repository.Database.Schema;

namespace SimpleCleanArch.Repository.Database.Context;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }

    public DbSet<ProductSchema> Products { get; set; }
}