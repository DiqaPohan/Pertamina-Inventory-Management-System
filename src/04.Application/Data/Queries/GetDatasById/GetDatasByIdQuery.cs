using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Extensions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDatasById;
public class GetDatasByIdQuery : IRequest<ListResponse<GetSingleData>>
{
    public string AppValue { get; set; }
    public string AppStatus { get; set; }
}
public class GetDatasByIdQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleData>
{
}
public class GetDatasByIdQueryHandler : IRequestHandler<GetDatasByIdQuery, ListResponse<GetSingleData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasByIdQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ListResponse<GetSingleData>> Handle(GetDatasByIdQuery request, CancellationToken cancellationToken)
    {
        var apps = await _context.Data
            .AsNoTracking()
                .Where(x => x.Code_Apps.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
            .ProjectTo<GetSingleData>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return apps.ToListResponse();
    }
}

