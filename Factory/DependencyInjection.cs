using SimpleCleanArch.Infra;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Repository.Implementations;
using SimpleCleanArch.Repository.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Application.CreateProduct;
using SimpleCleanArch.Application.GetProduct;
using SimpleCleanArch.Application.UpdateProduct;
using SimpleCleanArch.Application.DeleteProduct;
using SimpleCleanArch.Application.Contract;

namespace SimpleCleanArch.Factory;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
    {
        var sqliteConnection = configuration.GetConnectionString("SqliteConnection")
            ?? throw new Exception("missing sqlite configuration");
        services.AddDbContext<ProductsContext>(options => options.UseSqlite(sqliteConnection));
        services.AddScoped<ICreateProduct, CreateProduct>();
        services.AddScoped<IGetProduct, GetProduct>();
        services.AddScoped<IUpdateProduct, UpdateProduct>();
        services.AddScoped<IDeleteProduct, DeleteProduct>();
        services.AddScoped<IProductsRepository, ProductsRepositorySqlite>();
        services.AddScoped<ISendMailGateway, SendMailGateway>();
    }
}
