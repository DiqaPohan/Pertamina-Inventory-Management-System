using System.Globalization;
using CsvHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Attributes;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Audits.Queries.ExportAudits;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

namespace Pertamina.SolutionTemplate.Application.Audits.Queries.ExportAudits;

[Authorize(Policy = Permissions.SolutionTemplate_Audit_Index)]
public class ExportAuditsQuery : ExportAuditsRequest, IRequest<ExportAuditsResponse>
{
}

public class ExportAuditsQueryHandler : IRequestHandler<ExportAuditsQuery, ExportAuditsResponse>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IDateAndTimeService _dateTime;

    public ExportAuditsQueryHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<ExportAuditsResponse> Handle(ExportAuditsQuery request, CancellationToken cancellationToken)
    {
        var audits = await _context.Audits
                .Where(x => request.AuditIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(audits);

        var content = memoryStream.ToArray();

        return new ExportAuditsResponse
        {
            ContentType = ContentTypes.TextCsv,
            Content = content,
            FileName = $"Audits_{audits.Count}_{_dateTime.Now:yyyyMMdd_HHmmss}.csv"
        };
    }
}
