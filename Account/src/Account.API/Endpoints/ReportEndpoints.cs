using Account.Application.UseCases.Reports.Get.Queries;
using Carter;
using FluentValidation;
using MediatR;

namespace Account.API.Endpoints;

public class ReportEndpoints : CarterModule
{
    private const string BasePath = "/api/reports";

    public ReportEndpoints()
        : base(BasePath)
    {

    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (int clientId, 
            DateTime startDate, 
            DateTime endDate,
            IValidator <GetClientReportQuery> validator,
            ISender sender
            ) =>
        {
            var command = new GetClientReportQuery(clientId, startDate, endDate);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var report = await sender.Send(command);

            return Results.Ok(report);
        });
    }
}
