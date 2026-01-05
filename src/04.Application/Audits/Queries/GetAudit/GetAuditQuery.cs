using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Attributes;
using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Audits.Constants;
using Pertamina.SolutionTemplate.Shared.Audits.Queries.GetAudit;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

namespace Pertamina.SolutionTemplate.Application.Audits.Queries.GetAudit;

[Authorize(Policy = Permissions.SolutionTemplate_Audit_View)]
public class GetAuditQuery : IRequest<GetAuditResponse>
{
    public Guid AuditId { get; set; }
}

public class GetAuditResponseMapping : IMapFrom<Audit, GetAuditResponse>
{
}

public class GetAuditQueryHandler : IRequestHandler<GetAuditQuery, GetAuditResponse>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;

    public GetAuditQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAuditResponse> Handle(GetAuditQuery request, CancellationToken cancellationToken)
    {
        var audit = await _context.Audits
            .AsNoTracking()
            .Where(x => x.Id == request.AuditId)
            .SingleOrDefaultAsync(cancellationToken);

        if (audit is null)
        {
            throw new NotFoundException(DisplayTextFor.Audit, request.AuditId);
        }

        return _mapper.Map<GetAuditResponse>(audit);
    }
}
