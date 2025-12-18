using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataWithToken;
using Pertamina.SolutionTemplate.Shared.Data.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetData;
public class GetDataByNameWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppNama { get; set; }
    public string AppStatus { get; set; }
}
public class GetDataByNameWithTokenQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataWithToken>
{
}
public class GetDataByNameWithTokenQueryHandler : IRequestHandler<GetDataByNameWithTokenQuery, OutputGetDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDataByNameWithTokenQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetDataByTokenData> Handle(GetDataByNameWithTokenQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataByTokenData();
        try
        {
            var apps = await _context.Data
           .AsNoTracking()
            .Where(x => x.Application_Name.Contains(request.AppNama) && x.Application_Status == request.AppStatus)
           .ProjectTo<GetSingleDataWithToken>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
            var app = new GetSingleDataWithToken();
            if (apps.Count > 0)
            {
                try
                {
                    app = apps.FirstOrDefault();
                    output.ResponseCode = "S";
                    output.ResponseMessage = "sukses";
                    output.Tanggal = System.DateTime.Now;
                    output.Items = new List<GetSingleDataWithToken>
            {
                app
            };
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

            }
            else
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "tidak ada data dengan nama aplikasi berikut " + request.AppNama;
                output.Tanggal = System.DateTime.Now;
                output.Items = new List<GetSingleDataWithToken>();
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
