//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Pertamina.SolutionTemplate.Application.Common.Extensions;
//using Pertamina.SolutionTemplate.Application.Common.Mappings;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
//using Pertamina.SolutionTemplate.Shared.Common.Responses;
//using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataHistoricalApplicationPhase;

//namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetHistoricalApplicationPhase;
//public class GetHistoricalApplicationPhaseQuery : IRequest<ListResponse<GetSingleDataHistoricalApplicationPhase>>
//{
//}
//public class GetHistoricalApplicationPhaseQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>
//{
//}
//public class GetHistoricalApplicationPhaseQueryHandler : IRequestHandler<GetHistoricalApplicationPhaseQuery, ListResponse<GetSingleDataHistoricalApplicationPhase>>
//{
//    private readonly ISolutionTemplateDbContext _context;
//    private readonly IMapper _mapper;
//    public GetHistoricalApplicationPhaseQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }
//    public async Task<ListResponse<GetSingleDataHistoricalApplicationPhase>> Handle(GetHistoricalApplicationPhaseQuery request, CancellationToken cancellationToken)
//    {
//        var apps = await _context.HistoricalAppPhase
//            .AsNoTracking()
//            .ProjectTo<GetSingleDataHistoricalApplicationPhase>(_mapper.ConfigurationProvider)
//            .ToListAsync(cancellationToken);
//        return apps.ToListResponse();
//    }
//}
