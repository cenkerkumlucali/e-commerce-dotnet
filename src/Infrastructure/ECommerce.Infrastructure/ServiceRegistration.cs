using System.Threading.Tasks.Dataflow;
using ECommerce.Application.Abstractions.Storage;
using ECommerce.Infrastructure.Enums;
using ECommerce.Infrastructure.Services.Storage;
using ECommerce.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }

    public static IServiceCollection AddStorage<T>(this IServiceCollection services) where T: class,IStorage
    {
        services.AddScoped<IStorage, T>();
        return services;
    }
    
    public static IServiceCollection AddStorage(this IServiceCollection services,StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                services.AddScoped<IStorage,LocalStorage>();
                break;
            case StorageType.Azure:
                break;
            case StorageType.AWS:
                break;
            default:
                services.AddScoped<IStorage,LocalStorage>();
                break;
        }
        return services;
    }
}