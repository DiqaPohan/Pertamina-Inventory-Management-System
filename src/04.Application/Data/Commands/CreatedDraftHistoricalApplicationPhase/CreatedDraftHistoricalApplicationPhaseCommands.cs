//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using FluentValidation;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
//using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;

//namespace Pertamina.SolutionTemplate.Application.Data.Commands.CreatedDraftHistoricalApplicationPhase;
////[Authorize(Policy = Permissions.KpiEnterprise_Catalog_View)]
//public class CreatedDraftHistoricalApplicationPhaseCommands : AddDraftHistoricalApplicationPhaseRequest, IRequest<AddDraftHistoricalApplicationPhaseResponse>
//{
//}

//public class CreatedDraftHistoricalApplicationPhaseCommandsValidator : AbstractValidator<CreatedDraftHistoricalApplicationPhaseCommands>
//{
//    public CreatedDraftHistoricalApplicationPhaseCommandsValidator()
//    {
//        Include(new AddHistoricalApplicationPhaseRequestValidator());
//    }
//}

//public class CreatedDraftHistoricalApplicationPhaseCommandsHandler : IRequestHandler<CreatedDraftHistoricalApplicationPhaseCommands, AddDraftHistoricalApplicationPhaseResponse>
//{
//    private readonly ISolutionTemplateDbContext _context;
//    private readonly IDateAndTimeService _dateTime;
//    private readonly IMapper _mapper;

//    public CreatedDraftHistoricalApplicationPhaseCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
//        });
//        mapper = config.CreateMapper();
//        _context = context;
//        _dateTime = dateTime;
//        _mapper = mapper;
//    }

//    public async Task<AddDraftHistoricalApplicationPhaseResponse> Handle(CreatedDraftHistoricalApplicationPhaseCommands request, CancellationToken cancellationToken)
//    {
//        var codeapps = "";
//        var apps = await _context.DraftHistoricalAppPhase
//         .AsNoTracking()
//          .ProjectTo<GetSingleDraftHistoricalData>(_mapper.ConfigurationProvider)
//        .ToListAsync(cancellationToken);
//        if (apps != null)
//        {
//            long convint = 0;
//            foreach (var singledata in apps)
//            {
//                try
//                {
//                    var lastcodes = singledata.Code_Apps.Replace("CHK-", "");
//                    var convlastcode = long.Parse(lastcodes);
//                    if (convlastcode > convint)
//                    {
//                        convint = convlastcode;
//                    }
//                }
//                catch
//                {

//                }
//            }

//            convint += 1;
//            codeapps = "CHK-" + convint.ToString();
//        }

//        if (!string.IsNullOrEmpty(request.Date.ToString()))
//        {
//            if (request.Date.ToString().Contains("0001"))
//            {
//                request.Date = System.DateTime.Now;
//            }
//        }
//        else
//        {
//            request.Date = System.DateTime.Now;
//            request.Day = request.Date.Day;
//            request.Month = request.Date.Month;
//            request.Year = request.Date.Year;
//        }

//        if (string.IsNullOrEmpty(request.Keterangan))
//        {
//            request.Keterangan = "";
//        }

//        if (!string.IsNullOrEmpty(request.Created_Date.ToString()))
//        {
//            if (request.Created_Date.ToString().Contains("0001"))
//            {
//                request.Created_Date = System.DateTime.Now;
//            }
//        }
//        else
//        {
//            request.Created_Date = System.DateTime.Now;
//        }

//        var data = new DraftHistoricalAppPhase
//        {
//            Id = 0,
//            Code_Apps = codeapps,
//            Code_Apps_Update = request.Code_Apps_Update,
//            Name_Apps_Update = request.Name_Apps_Update,
//            Phase = request.Phase,
//            Date = request.Date,
//            Day = request.Day,
//            Month = request.Month,
//            Year = request.Year,
//            IsApproved = request.IsApproved,
//            Source = request.Source,
//            Reason = request.Reason,
//            Approved_Status = request.Approved_Status,
//            CreatedBy = request.Created_By,
//            Created = request.Created_Date,
//            IsDeleted = request.IsDeleted,
//        };

//        await _context.DraftHistoricalAppPhase.AddAsync(data, cancellationToken);
//        await _context.SaveChangesAsync(cancellationToken);

//        return new AddDraftHistoricalApplicationPhaseResponse
//        {
//            Code_App = codeapps
//        };
//    }
//}
