using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Extensions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetRequestData;
public class GetRequestDataQuery : IRequest<ListResponse<GetSingleRequestData>>
{
}
public class GetRequestDataQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.RequestData, GetSingleRequestData>
{
}
public class GetRequestDataQueryHandler : IRequestHandler<GetRequestDataQuery, ListResponse<GetSingleRequestData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetRequestDataQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ListResponse<GetSingleRequestData>> Handle(GetRequestDataQuery request, CancellationToken cancellationToken)
    {
        var apps = await _context.RequestData
            .AsNoTracking()
            .ProjectTo<GetSingleRequestData>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return apps.ToListResponse();
    }
}
