using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ApplicationServiceRegistration));
        services.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ApplicationRegistration>());
        services.AddHttpClient();
        return services;
    }
}

public class ApplicationRegistration
{
}