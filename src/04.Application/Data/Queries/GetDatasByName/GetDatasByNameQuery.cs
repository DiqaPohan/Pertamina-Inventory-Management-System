using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Extensions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDatasByName;

public class GetDatasByNameQuery : IRequest<ListResponse<GetSingleData>>
{
    public string AppValue { get; set; }
    public string AppStatus { get; set; }
}
public class GetDatasByNameQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleData>
{
}
public class GetDatasByNameQueryHandler : IRequestHandler<GetDatasByNameQuery, ListResponse<GetSingleData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasByNameQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ListResponse<GetSingleData>> Handle(GetDatasByNameQuery request, CancellationToken cancellationToken)
    {
        var apps = await _context.Data
            .AsNoTracking()
                .Where(x => x.Application_Name.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
            .ProjectTo<GetSingleData>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return apps.ToListResponse();
    }
}

