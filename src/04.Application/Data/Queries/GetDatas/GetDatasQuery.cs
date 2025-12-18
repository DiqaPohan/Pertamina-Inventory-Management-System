using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Extensions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDatas;

public class GetDatasQuery : IRequest<ListResponse<GetSingleData>>
{
}
public class GetDatasQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleData>
{
}
public class GetDatasQueryHandler : IRequestHandler<GetDatasQuery, ListResponse<GetSingleData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ListResponse<GetSingleData>> Handle(GetDatasQuery request, CancellationToken cancellationToken)
    {
        var apps = await _context.Data
            .AsNoTracking()
            .ProjectTo<GetSingleData>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return apps.ToListResponse();
    }
}
