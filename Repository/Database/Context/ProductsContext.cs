using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Repository.Database.Schema;

namespace SimpleCleanArch.Repository.Database.Context;

public class ProductsContext(DbContextOptions<ProductsContext> options) : DbContext(options)
{
    public DbSet<ProductSchema> Products { get; set; }
}