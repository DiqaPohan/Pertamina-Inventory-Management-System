using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetDataByIDWithToken;
public class GetDataByIDWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppID { get; set; }
    public string AppStatus { get; set; }
}
public class GetDataByIDWithTokenQueryResponseMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, OutputGetDataByTokenData>
{
}
public class GetDataByIDWithTokenQueryHandler : IRequestHandler<GetDataByIDWithTokenQuery, OutputGetDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDataByIDWithTokenQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetDataByTokenData> Handle(GetDataByIDWithTokenQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataByTokenData();
        try
        {
            var apps = await _context.Data
           .AsNoTracking()
            .Where(x => x.Code_Apps.Contains(request.AppID) && x.Application_Status == request.AppStatus)
           .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
            var app = new GetSingleDataData();
            if (apps.Count > 0)
            {
                try
                {
                    app = apps.FirstOrDefault();
                    output.ResponseCode = "S";
                    output.ResponseMessage = "Sukses";
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
                output.ResponseCode = "S";
                output.ResponseMessage = "tidak ada data dengan code aplikasi berikut " + request.AppID;
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

