using FluentValidation;
using HelpDesk.Application.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDesk.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        { 
            cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(Extensions).Assembly);
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(Extensions).Assembly);
        });

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}
