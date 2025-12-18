//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using FluentValidation;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Pertamina.SolutionTemplate.Application.Common.Attributes;
//using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
//using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
//using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
//using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

//namespace Pertamina.SolutionTemplate.Application.Data.Commands.UpdatedDraftHistoricalApplicationPhase;
//[Authorize(Policy = Permissions.KpiEnterprise_Catalog_Approval)]
//public class UpdatedDraftHistoricalApplicationPhaseCommands : AddDraftHistoricalApplicationPhaseRequest, IRequest<AddDraftHistoricalApplicationPhaseResponse>
//{
//}

//public class UpdatedDraftHistoricalApplicationPhaseValidator : AbstractValidator<UpdatedDraftHistoricalApplicationPhaseCommands>
//{
//    public UpdatedDraftHistoricalApplicationPhaseValidator()
//    {
//        Include(new AddHistoricalApplicationPhaseRequestValidator());
//    }
//}

//public class UpdatedDraftHistoricalApplicationPhaseCommandsHandler : IRequestHandler<UpdatedDraftHistoricalApplicationPhaseCommands, AddDraftHistoricalApplicationPhaseResponse>
//{
//    private readonly ISolutionTemplateDbContext _context;
//    private readonly IDateAndTimeService _dateTime;
//    private readonly IMapper _mapper;

//    public UpdatedDraftHistoricalApplicationPhaseCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
//    {
//        //var config = new MapperConfiguration(cfg =>
//        //{
//        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
//        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDraftHistoricalData>();
//        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
//        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
//        //});
//        var config = new MapperConfiguration(cfg =>
//        {
//            //   cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
//            //   cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDraftHistoricalData>();
//            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
//            //   cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
//        });
//        mapper = config.CreateMapper();
//        _context = context;
//        _dateTime = dateTime;
//        _mapper = mapper;
//    }

//    public async Task<AddDraftHistoricalApplicationPhaseResponse> Handle(UpdatedDraftHistoricalApplicationPhaseCommands request, CancellationToken cancellationToken)
//    {
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

//        var codeapps = "";
//        var apps = await _context.Data
//       .AsNoTracking()
//        .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
//      .ToListAsync(cancellationToken);
//        if (apps != null)
//        {
//            long convint = 0;
//            foreach (var singledata in apps)
//            {
//                try
//                {
//                    var lastcodes = singledata.Code_Apps.Replace("RP-", "");
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
//            codeapps = "RP-" + convint.ToString();
//            request.Code_Apps_Update = "RP-" + convint.ToString();
//        }

//        var codeappsdrafthistory = request.Code_Apps;
//        var app = await _context.DraftHistoricalAppPhase
//           .Where(pp => pp.Code_Apps == request.Code_Apps)
//           .SingleOrDefaultAsync(cancellationToken);
//        if (app is not null)
//        {
//            if (string.IsNullOrEmpty(app.Code_Apps_Update))
//            {
//                app.Code_Apps_Update = request.Code_Apps_Update;
//            }

//            app.IsApproved = request.IsApproved;
//            app.Approved_Status = request.Approved_Status;
//            app.Reason = request.Reason;
//            await _context.SaveChangesAsync(cancellationToken);

//        }
//        #region ApprovedData
//        if (request.IsApproved == "Approved")
//        {
//            #region CreateHistoricalMasterData
//            var datahistoricalinsert = new Domain.Entities.HistoricalAppPhase
//            {
//                Id = 0,
//                Code_Apps = request.Code_Apps_Update,
//                Phase = request.Phase,
//                Source = request.Source,
//                CreatedBy = request.Created_By,
//                IsDeleted = request.IsDeleted,
//                Date = request.Date,
//                Day = request.Date.Day,
//                Month = request.Date.Month,
//                Year = request.Date.Year
//            };
//            await _context.HistoricalAppPhase.AddAsync(datahistoricalinsert, cancellationToken);
//            await _context.SaveChangesAsync(this, cancellationToken);

//            #endregion

//        }

//        #endregion
//        return new AddDraftHistoricalApplicationPhaseResponse
//        {
//            Code_App = codeappsdrafthistory
//        };
//    }
//}
