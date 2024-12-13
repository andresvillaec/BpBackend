using MediatR;

namespace Account.Application.UseCases.Reports.Get.Queries;

public record GetClientReportQuery(
    int ClientId,
     DateTime StartDate,
     DateTime EndDate) 
    : IRequest<List<ReportResponse>>
{
}

public record ReportResponse(
    string AccountNumber,
    decimal TotalBalance,
    IEnumerable<MovementItemResponse> Movements);

public record MovementItemResponse(
    decimal Amount,
    decimal Balance,
    string MovementType,
    DateTime Timestamp);