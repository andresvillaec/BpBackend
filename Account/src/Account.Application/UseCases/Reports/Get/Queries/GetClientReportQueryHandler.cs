using Account.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Account.Application.UseCases.Reports.Get.Queries;

internal sealed class GetClientReportQueryHandler : IRequestHandler<GetClientReportQuery, List<ReportResponse>>
{
    private readonly IApplicantionDbContext _context;

    public GetClientReportQueryHandler(IApplicantionDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReportResponse>> Handle(GetClientReportQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movements
             .Where(FilterReport(request)) // Filter by ClientId
             .GroupBy(a => a.Account.Number) // Group by AccountNumber
             .Select(group =>
                 new ReportResponse(
                    group.Key,
                    group.Select(movement =>
                        new MovementItemResponse(
                                movement.Amount,
                                movement.Balance,
                                movement.Amount > 0 ? "Deposito" : "Retiro",
                                movement.Timestamp
                        )
                    )
                 )
             )
             .ToListAsync(cancellationToken);
    }

    private Expression<Func<Domain.Entities.Movement, bool>> FilterReport(GetClientReportQuery request)
    {
        return a => a.Account.ClientId == request.ClientId 
            && a.Timestamp.Date >= request.StartDate.Date 
            && a.Timestamp.Date <= request.EndDate.Date;
    }
}
