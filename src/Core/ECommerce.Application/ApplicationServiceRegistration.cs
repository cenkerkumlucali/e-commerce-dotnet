using System.Collections.Immutable;
using ECommerce.Application.Validators;
using ECommerce.Application.ViewModels.Products;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddFluentValidation(configuration =>
            configuration.RegisterValidatorsFromAssemblyContaining<ApplicationRegistration>());
        
        return services;
    }
}
public class ApplicationRegistration
{
}

