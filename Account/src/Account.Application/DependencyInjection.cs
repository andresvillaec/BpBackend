using Account.Application.Handlers;
using Account.Application.UseCases.BankAccount.Commands.Create;
using Account.Application.UseCases.BankAccount.Commands.Update;
using Account.Application.UseCases.Movement.Commands.Create;
using Account.Application.UseCases.Movement.Commands.Update;
using Account.Application.UseCases.Reports.Get.Queries;
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

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommandHandler).Assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAccountValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateMovementValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateMovementValidator>();

        services.AddValidatorsFromAssemblyContaining<GetClientReportValidator>();

        services.AddHttpClient<ClientExistsQueryHandler>();

        return services;
    }
}
