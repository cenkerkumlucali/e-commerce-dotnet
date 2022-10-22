using System.Configuration;
using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.File;
using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repositories;
using ECommerce.Persistence.Repositories.File;
using ECommerce.Persistence.Repositories.InvoiceFile;
using ECommerce.Persistence.Repositories.ProductImageFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        
        services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        
        services.AddScoped<IOrderReadRepository,OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        
        services.AddScoped<IProductReadRepository,ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        
        services.AddScoped<IFileReadRepository,FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        
        services.AddScoped<IProductImageReadRepository,ProductImageReadRepository>();
        services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
        
        services.AddScoped<IInvoiceReadRepository,InvoiceReadRepository>();
        services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();
        return services;
    }
}