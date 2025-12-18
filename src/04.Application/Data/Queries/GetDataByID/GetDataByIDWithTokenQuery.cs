using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataWithToken;
using Pertamina.SolutionTemplate.Shared.Data.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDataByID;
public class GetDataByIDWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppID { get; set; }
    public string AppStatus { get; set; }
}
public class GetDataByIDWithTokenQueryResponseMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataWithToken>
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
                output.ResponseMessage = "tidak ada data dengan code aplikasi berikut " + request.AppID;
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

