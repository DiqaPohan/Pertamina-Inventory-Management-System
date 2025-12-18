using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;

using Pertamina.SolutionTemplate.Shared.Data.Constants;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetData;
//[Authorize(Policy = Permissions.SolutionTemplate_App_View)]
public class GetDataQuery : IRequest<GetSingleData>
{
    public string AppNama { get; set; }
    public string AppStatus { get; set; }
}
public class GetAppResponseMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleData>
{
}
public class GetDataQueryHandler : IRequestHandler<GetDataQuery, GetSingleData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDataQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetSingleData> Handle(GetDataQuery request, CancellationToken cancellationToken)
    {
        //var app = await _context.Data.AsNoTracking()
        //    .Where(x => x.Application_Name == request.AppNama)
        //    .SingleOrDefaultAsync(cancellationToken);
        var apps = await _context.Data
          .AsNoTracking()
           .Where(x => x.Application_Name.Contains(request.AppNama) && x.Application_Status == request.AppStatus)
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
                throw new NotFoundException(DisplayTextFor.Data, request.AppNama);
            }

        }
        else
        {
            throw new NotFoundException(DisplayTextFor.Data, request.AppNama);
        }

        return _mapper.Map<GetSingleData>(app);
    }
}
