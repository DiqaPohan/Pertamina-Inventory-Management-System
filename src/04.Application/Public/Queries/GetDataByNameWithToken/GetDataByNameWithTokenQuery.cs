using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetDataByNameWithToken;
public class GetDataByNameWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppNama { get; set; }
    public string AppStatus { get; set; }
}
public class GetDataByNameWithTokenQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, OutputGetDataByTokenData>
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
           .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
            var app = new GetSingleDataData();
            if (apps.Count > 0)
            {
                try
                {
                    app = apps.FirstOrDefault();
                    output.ResponseCode = "S";
                    output.ResponseMessage = "sukses";
                    output.Tanggal = System.DateTime.Now;
                    output.Items = new List<GetSingleDataData>
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
                    output.Items = new List<GetSingleDataData>();
                }

            }
            else
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "tidak ada data dengan nama aplikasi berikut " + request.AppNama;
                output.Tanggal = System.DateTime.Now;
                output.Items = new List<GetSingleDataData>();
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
