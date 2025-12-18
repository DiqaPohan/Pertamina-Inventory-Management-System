using Pertamina.SolutionTemplate.Shared.Common.Attributes;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Shared.Data.Commands.DetailData;

public class DetailDataRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Appdesc { get; set; } = default!;
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Appowner { get; set; } = default!;
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Appownerpic { get; set; } = default!;
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Appownerdev { get; set; } = default!;
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Applink { get; set; } = default!;
}
