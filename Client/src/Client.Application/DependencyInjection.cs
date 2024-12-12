using Client.Application.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateClientCommandValidator>();

        return services;
    }
}
