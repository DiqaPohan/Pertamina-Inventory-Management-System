using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetDatasWithToken;
public class GetDatasWithTokenQuery : IRequest<OutputGetDataByTokenData>
{
    public string AppKey { get; set; }
    public string AppValue { get; set; }
    public string AppStatus { get; set; }
}
public class GetDatasWithTokenQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.Data, OutputGetDataByTokenData>
{
}
public class GetDatasWithTokenQueryHandler : IRequestHandler<GetDatasWithTokenQuery, OutputGetDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetDatasWithTokenQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetDataByTokenData> Handle(GetDatasWithTokenQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataByTokenData();
        try
        {
            if (!string.IsNullOrEmpty(request.AppKey))
            {
                if (request.AppKey == "id")
                {
                    if (!string.IsNullOrEmpty(request.AppStatus))
                    {
                        var apps = await _context.Data
         .AsNoTracking()
          .Where(x => x.Code_Apps.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
         .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
         .ToListAsync(cancellationToken);
                        if (apps.Count > 0)
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "Sukses";
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                            output.Items.AddRange(apps);
                        }
                        else
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "tidak ada data dengan code aplikasi berikut " + request.AppValue;
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                        }
                    }
                    else
                    {
                        var apps = await _context.Data
    .AsNoTracking()
     .Where(x => x.Code_Apps.Contains(request.AppValue))
    .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);
                        if (apps.Count > 0)
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "sukses";
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                            output.Items.AddRange(apps);
                        }
                        else
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "tidak ada data dengan code aplikasi berikut " + request.AppValue;
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                        }

                    }

                }
                else if (request.AppKey == "name")
                {
                    if (!string.IsNullOrEmpty(request.AppStatus))
                    {
                        var apps = await _context.Data
         .AsNoTracking()
          .Where(x => x.Application_Name.Contains(request.AppValue) && x.Application_Status == request.AppStatus)
         .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
         .ToListAsync(cancellationToken);
                        if (apps.Count > 0)
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "Sukses";
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                            output.Items.AddRange(apps);
                        }
                        else
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "tidak ada data dengan nama aplikasi berikut " + request.AppValue;
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                        }
                    }
                    else
                    {
                        var apps = await _context.Data
     .AsNoTracking()
      .Where(x => x.Application_Name.Contains(request.AppValue))
     .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
     .ToListAsync(cancellationToken);
                        if (apps.Count > 0)
                        {
                            output.ResponseCode = "S";
                            output.ResponseMessage = "sukses";
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                            output.Items.AddRange(apps);
                        }
                        else
                        {
                            output.ResponseCode = "E";
                            output.ResponseMessage = "tidak ada data dengan nama aplikasi berikut " + request.AppValue;
                            output.Tanggal = System.DateTime.Now;
                            output.Items = new List<GetSingleDataData>();
                        }
                    }
                }
                else
                {
                    output.ResponseCode = "E";
                    output.ResponseMessage = "Parameter key tidak di kenal harap isi dengan id atau name";
                    output.Tanggal = System.DateTime.Now;
                    output.Items = new List<GetSingleDataData>();
                }

            }
            else
            {
                var apps = await _context.Data
          .AsNoTracking()
          .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
                var app = new GetSingleDataData();
                if (apps.Count > 0)
                {
                    output.ResponseCode = "S";
                    output.ResponseMessage = "Sukses";
                    output.Tanggal = System.DateTime.Now;
                    output.Items = new List<GetSingleDataData>();
                    output.Items.AddRange(apps);
                }
                else
                {
                    output.ResponseCode = "S";
                    output.ResponseMessage = "Master data kosong";
                    output.Tanggal = System.DateTime.Now;
                    output.Items = new List<GetSingleDataData>();
                }
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
