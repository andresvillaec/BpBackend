using Account.Application.UseCases.BankAccount.Commands.Create;
using Account.Application.UseCases.BankAccount.Commands.Update;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

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
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAccountValidator>();

        return services;
    }
}
