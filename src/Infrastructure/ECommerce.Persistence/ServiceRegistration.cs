using System.Configuration;
using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.Abstractions.Services.Authentications;
using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Basket;
using ECommerce.Application.Repositories.BasketItem;
using ECommerce.Application.Repositories.CompletedOrder;
using ECommerce.Application.Repositories.Customer;
using ECommerce.Application.Repositories.File;
using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Application.Repositories.Order;
using ECommerce.Application.Repositories.Product;
using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repositories;
using ECommerce.Persistence.Repositories.Basket;
using ECommerce.Persistence.Repositories.BasketItem;
using ECommerce.Persistence.Repositories.CompletedOrder;
using ECommerce.Persistence.Repositories.File;
using ECommerce.Persistence.Repositories.InvoiceFile;
using ECommerce.Persistence.Repositories.ProductImageFile;
using ECommerce.Persistence.Services;
using Microsoft.AspNetCore.Identity;
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
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ECommerceDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
        services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();

        services.AddScoped<IInvoiceReadRepository, InvoiceReadRepository>();
        services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();

        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

        services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
        
        services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
        services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();

        return services;
    }
}