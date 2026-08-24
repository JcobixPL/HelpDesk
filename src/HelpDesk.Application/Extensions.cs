using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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

        return services;
    }
}
