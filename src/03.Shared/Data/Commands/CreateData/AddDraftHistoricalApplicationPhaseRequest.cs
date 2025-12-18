using FluentValidation;
using Pertamina.SolutionTemplate.Shared.Common.Attributes;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
public class AddDraftHistoricalApplicationPhaseRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Code_Apps { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Code_Apps_Update { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Name_Apps_Update { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Phase { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public DateTime Date { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public int Year { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public int Month { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public int Day { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Keterangan { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Source { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Reason { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Approved_Status { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? IsApproved { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public DateTimeOffset Created_Date { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Created_By { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? IsDeleted { get; set; }
}

public class AddHistoricalApplicationPhaseRequestValidator : AbstractValidator<AddDraftHistoricalApplicationPhaseRequest>
{

    public AddHistoricalApplicationPhaseRequestValidator()
    {
        _ = RuleFor(x => x.Code_Apps)
            .NotEmpty();

        _ = RuleFor(x => x.Phase)
          .NotEmpty();

    }
}
