using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleDataApplicationType;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetApplicationTypeByTokenData;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetAllDataApplicationType;
public class GetAllDataApplicationTypeQuery : IRequest<OutputGetApplicationTypeByTokenData>
{

}
public class GetAllDataApplicationTypeQueryMapping : IMapFrom<Pertamina.SolutionTemplate.Domain.Entities.ApplicationType, GetSingleDataApplicationType>
{
}
public class GetAllDataApplicationTypeQueryHandler : IRequestHandler<GetAllDataApplicationTypeQuery, OutputGetApplicationTypeByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDataApplicationTypeQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetApplicationTypeByTokenData> Handle(GetAllDataApplicationTypeQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetApplicationTypeByTokenData();
        try
        {
            var apps = await _context.ApplicationType
          .AsNoTracking()
          .ProjectTo<GetSingleDataApplicationType>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
            if (apps.Count > 0)
            {
                output.ResponseCode = "S";
                output.ResponseMessage = "Sukses";
                output.Items = new List<GetSingleDataApplicationType>();
                output.Items.AddRange(apps.ToList());
                output.Tanggal = System.DateTime.Now;
            }
            else
            {
                output.ResponseCode = "S";
                output.ResponseMessage = "Master data kosong";
                output.Items = new List<GetSingleDataApplicationType>();
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
            output.Items = new List<GetSingleDataApplicationType>();
        }

        return output;
    }
}

