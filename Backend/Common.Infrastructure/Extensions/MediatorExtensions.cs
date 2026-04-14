using System.Reflection;
using Common.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? mediatorKey, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            if (!string.IsNullOrEmpty(mediatorKey))
            {
                cfg.LicenseKey = mediatorKey;
            }
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}