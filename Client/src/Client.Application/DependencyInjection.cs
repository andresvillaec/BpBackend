using Client.Application.Client.Commands.Create;
using Client.Application.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Client.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();

            config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        //services.AddScoped<IValidator<CreateClientCommand>, CreateClientCommandValidator>();
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateClientCommandValidator>();

        return services;
    }
}
