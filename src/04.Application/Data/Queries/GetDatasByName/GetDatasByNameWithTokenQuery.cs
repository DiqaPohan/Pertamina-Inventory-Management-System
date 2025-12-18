using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataWithToken;
using Pertamina.SolutionTemplate.Shared.Data.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDatasByName;
public class GetDatasByNameWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppValue { get; set; }
    public string AppStatus { get; set; }
}
public class GetDatasByNameWithTokenQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataWithToken>
{
}
public class GetDatasByNameWithTokenQueryHandler : IRequestHandler<GetDatasByNameWithTokenQuery, OutputGetDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasByNameWithTokenQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetDataByTokenData> Handle(GetDatasByNameWithTokenQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataByTokenData();
        try
        {
            var apps = await _context.Data
           .AsNoTracking()
               .Where(x => x.Application_Name.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
           .ProjectTo<GetSingleDataWithToken>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

            if (apps.Count > 0)
            {
                output.ResponseCode = "S";
                output.ResponseMessage = "sukses";
                output.Items = new List<GetSingleDataWithToken>();
                output.Items.AddRange(apps.ToList());
                output.Tanggal = System.DateTime.Now;
            }
            else
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "tidak ada data dengan nama aplikasi berikut " + request.AppValue;
                output.Items = new List<GetSingleDataWithToken>();
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
            output.Items = new List<GetSingleDataWithToken>();
        }

        return output;
    }
}

