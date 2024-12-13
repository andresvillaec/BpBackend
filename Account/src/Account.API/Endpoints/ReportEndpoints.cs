using Account.Application.UseCases.Reports.Get.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        app.MapGet("", async (int clientId, DateTime startDate, DateTime endDate, ISender sender) =>
        {
            var report = await sender.Send(new GetClientReportQuery(
                clientId,
                startDate,
                endDate));

            return Results.Ok(report);
        });
    }
}
