using Pertamina.SolutionTemplate.Shared.Common.Attributes;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Shared.Audits.Queries.ExportAudits;

public class ExportAuditsRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public IList<Guid> AuditIds { get; set; } = new List<Guid>();
}
