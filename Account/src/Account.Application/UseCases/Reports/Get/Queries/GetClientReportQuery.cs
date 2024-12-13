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
    //decimal TotalBalance,
    IEnumerable<MovementItemResponse> Movements);

public record MovementItemResponse(
    decimal Amount,
    decimal Balance,
    string MovementType,
    DateTime Timestamp);

//public record ClientReportRequest(
//    DateTime StartDate,
//    DateTime EndDate,
//    int ClientId);

public class ClientReportRequest : IParsable<ClientReportRequest>
{
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Parse method (used internally)
    public static ClientReportRequest Parse(string value, IFormatProvider formatProvider)
    {
        // Parsing logic from a single string (if applicable)
        throw new NotImplementedException();
    }

    // TryParse method (required by IParsable)
    public static bool TryParse(string value, IFormatProvider formatProvider, out ClientReportRequest result)
    {
        // Example: Deserialize JSON from query string (adjust as needed)
        try
        {
            result = System.Text.Json.JsonSerializer.Deserialize<ClientReportRequest>(value);
            return result != null;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}