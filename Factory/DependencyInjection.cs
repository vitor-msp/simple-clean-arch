using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.UseCases;
using SimpleCleanArch.Application.Contract.Gateways;
using SimpleCleanArch.Infra;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Repository.Implementations;
using SimpleCleanArch.Repository.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SimpleCleanArch.Factory;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
    {
        var sqliteConnection = configuration.GetConnectionString("SqliteConnection") ?? "";
        services.AddDbContext<ProductsContext>(options => options.UseSqlite(sqliteConnection));
        services.AddScoped<ICreateProduct, CreateProduct>();
        services.AddScoped<IGetProduct, GetProduct>();
        services.AddScoped<IDeleteProduct, DeleteProduct>();
        // services.AddSingleton<IProductsRepository, ProductsRepositoryMemory>();
        services.AddScoped<IProductsRepository, ProductsRepositorySqlite>();
        services.AddScoped<ISendMailGateway, SendMailGateway>();
    }
}
