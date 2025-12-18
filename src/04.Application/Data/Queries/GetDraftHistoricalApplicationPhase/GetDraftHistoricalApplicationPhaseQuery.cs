//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Pertamina.SolutionTemplate.Application.Common.Extensions;
//using Pertamina.SolutionTemplate.Application.Common.Mappings;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
//using Pertamina.SolutionTemplate.Shared.Common.Responses;
//using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataDraftHistoricalApplicationPhase;

//namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDraftHistoricalApplicationPhase;
//public class GetDraftHistoricalApplicationPhaseQuery : IRequest<ListResponse<GetSingleDataDraftHistoricalApplicationPhase>>
//{
//}
//public class GetDraftHistoricalApplicationPhaseQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDataDraftHistoricalApplicationPhase>
//{
//}
//public class GetDraftHistoricalApplicationPhaseQueryHandler : IRequestHandler<GetDraftHistoricalApplicationPhaseQuery, ListResponse<GetSingleDataDraftHistoricalApplicationPhase>>
//{
//    private readonly ISolutionTemplateDbContext _context;
//    private readonly IMapper _mapper;
//    public GetDraftHistoricalApplicationPhaseQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }
//    public async Task<ListResponse<GetSingleDataDraftHistoricalApplicationPhase>> Handle(GetDraftHistoricalApplicationPhaseQuery request, CancellationToken cancellationToken)
//    {
//        var apps = await _context.DraftHistoricalAppPhase
//            .AsNoTracking()
//            .ProjectTo<GetSingleDataDraftHistoricalApplicationPhase>(_mapper.ConfigurationProvider)
//            .ToListAsync(cancellationToken);
//        return apps.ToListResponse();
//    }
//}
