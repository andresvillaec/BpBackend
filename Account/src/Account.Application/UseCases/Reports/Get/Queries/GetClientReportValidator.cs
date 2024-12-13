using FluentValidation;

namespace Account.Application.UseCases.Reports.Get.Queries;

public sealed class GetClientReportValidator : AbstractValidator<GetClientReportQuery>
{
    public GetClientReportValidator()
    {
        RuleFor(x => x.ClientId)
            .NotNull()
            .GreaterThan(1);

        RuleFor(x => x.StartDate)
            .NotNull()
            .LessThanOrEqualTo(x => x.EndDate);

        RuleFor(x => x.EndDate)
            .NotNull()
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
}
