using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetDatasByIDWithToken;
public class GetDatasByIdWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppValue { get; set; }
    public string AppStatus { get; set; }
}
public class GetDatasByIdWithTokenQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>
{
}
public class GetDatasByIdWithTokenQueryHandler : IRequestHandler<GetDatasByIdWithTokenQuery, OutputGetDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasByIdWithTokenQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetDataByTokenData> Handle(GetDatasByIdWithTokenQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataByTokenData();
        try
        {
            var apps = await _context.Data
          .AsNoTracking()
           .Where(x => x.Code_Apps.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
          .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
            if (apps.Count > 0)
            {
                output.ResponseCode = "S";
                output.ResponseMessage = "sukses";
                output.Items = new List<GetSingleDataData>();
                output.Items.AddRange(apps.ToList());
                output.Tanggal = System.DateTime.Now;
            }
            else
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "tidak ada data dengan code aplikasi berikut " + request.AppValue;
                output.Items = new List<GetSingleDataData>();
                output.Tanggal = System.DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            output.ResponseCode = "E";

            if (ex.StackTrace.Count() >= 200)
            {
                output.ResponseMessage = ex.StackTrace[..200];
            }
            else
            {
                output.ResponseMessage = ex.StackTrace;
            }

            output.Tanggal = System.DateTime.Now;
            output.Items = new List<GetSingleDataData>();
        }

        return output;
    }
}

