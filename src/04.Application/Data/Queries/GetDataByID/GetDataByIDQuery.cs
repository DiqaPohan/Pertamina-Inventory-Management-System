using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Data.Constants;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDataByID;
public class GetDataByIDQuery : IRequest<GetSingleData>
{
    public string AppID { get; set; }
    public string AppStatus { get; set; }
}
public class GetDataByIDQueryResponseMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleData>
{
}
public class GetDataByIDQueryHandler : IRequestHandler<GetDataByIDQuery, GetSingleData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDataByIDQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetSingleData> Handle(GetDataByIDQuery request, CancellationToken cancellationToken)
    {
        var apps = await _context.Data
         .AsNoTracking()
          .Where(x => x.Code_Apps.Contains(request.AppID) && x.Application_Status == request.AppStatus)
         .ProjectTo<GetSingleData>(_mapper.ConfigurationProvider)
         .ToListAsync(cancellationToken);
        var app = new GetSingleData();
        if (apps.Count > 0)
        {
            try
            {
                app = apps.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new NotFoundException(DisplayTextFor.Data, request.AppID);
            }

        }
        else
        {
            throw new NotFoundException(DisplayTextFor.Data, request.AppID);
        }

        return _mapper.Map<GetSingleData>(app);
    }
}
