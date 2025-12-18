using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Commands.CreatedDraftCatalogData;
//[Authorize(Policy = Permissions.KpiEnterprise_Catalog_View)]
public class CreatedDraftCatalogDataCommands : AddDraftCatalogRequest, IRequest<AddDraftCatalogResponse>
{
}

public class CreatedDraftCatalogDataCommandsValidator : AbstractValidator<CreatedDraftCatalogDataCommands>
{
    public CreatedDraftCatalogDataCommandsValidator()
    {
        Include(new AddDraftCatalogRequestValidator());
    }
}

public class CreatedDraftCatalogDataCommandsHandler : IRequestHandler<CreatedDraftCatalogDataCommands, AddDraftCatalogResponse>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IDateAndTimeService _dateTime;
    private readonly IMapper _mapper;

    public CreatedDraftCatalogDataCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
    {
        //var config = new MapperConfiguration(cfg =>
        //{
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.RequestData, GetSingleRequestData>();
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
        //});
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.RequestData, GetSingleRequestData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _dateTime = dateTime;
        _mapper = mapper;
    }

    public async Task<AddDraftCatalogResponse> Handle(CreatedDraftCatalogDataCommands request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(request.Utilization))
        {
            request.Utilization = "";
        }

        if (string.IsNullOrEmpty(request.Application_License))
        {
            request.Application_License = "";
        }

        if (!string.IsNullOrEmpty(request.Application_Ats.ToString()))
        {
            if (request.Application_Ats.ToString().Contains("0001"))
            {
                request.Application_Ats = System.DateTime.Now;
            }
        }
        else
        {
            request.Application_Ats = System.DateTime.Now;
        }

        if (string.IsNullOrEmpty(request.Application_Package))
        {
            request.Application_Package = "";
        }

        if (string.IsNullOrEmpty(request.User_Manual_Document))
        {
            request.User_Manual_Document = "";
        }

        if (string.IsNullOrEmpty(request.Link_Application))
        {
            request.Link_Application = "";
        }

        if (string.IsNullOrEmpty(request.Users))
        {
            request.Users = "";
        }

        if (string.IsNullOrEmpty(request.Criticality))
        {
            request.Criticality = "";
        }

        if (string.IsNullOrEmpty(request.Service_Owner))
        {
            request.Service_Owner = "";
        }

        if (string.IsNullOrEmpty(request.Db_Server_Dev))
        {
            request.Db_Server_Dev = "";
        }

        if (string.IsNullOrEmpty(request.Db_Name_Dev))
        {
            request.Db_Name_Dev = "";
        }

        if (string.IsNullOrEmpty(request.App_Server_Dev))
        {
            request.App_Server_Dev = "";
        }

        if (string.IsNullOrEmpty(request.Db_Server_Prod))
        {
            request.Db_Server_Prod = "";
        }

        if (string.IsNullOrEmpty(request.App_Server_Prod))
        {
            request.App_Server_Prod = "";
        }

        if (string.IsNullOrEmpty(request.Db_Name_Prod))
        {
            request.Db_Name_Prod = "";
        }

        if (string.IsNullOrEmpty(request.Keterangan))
        {
            request.Keterangan = "";
        }

        if (string.IsNullOrEmpty(request.Company_Code))
        {
            request.Company_Code = "";
        }

        if (string.IsNullOrEmpty(request.Users))
        {
            request.Users = "";
        }

        if (string.IsNullOrEmpty(request.Code_Apps))
        {
            request.Code_Apps = "";
        }

        if (string.IsNullOrEmpty(request.Keterangan))
        {
            request.Keterangan = "";
        }

        if (string.IsNullOrEmpty(request.Criticality))
        {
            request.Criticality = "";
        }

        if (string.IsNullOrEmpty(request.Db_Name_Dev))
        {
            request.Db_Name_Dev = "";
        }

        if (string.IsNullOrEmpty(request.Utilization))
        {
            request.Utilization = "";
        }

        if (string.IsNullOrEmpty(request.Db_Name_Prod))
        {
            request.Db_Name_Prod = "";
        }

        if (string.IsNullOrEmpty(request.Db_Server_Dev))
        {
            request.Db_Server_Dev = "";
        }

        if (string.IsNullOrEmpty(request.Service_Owner))
        {
            request.Service_Owner = "";
        }

        if (string.IsNullOrEmpty(request.App_Server_Dev))
        {
            request.App_Server_Dev = "";
        }

        if (string.IsNullOrEmpty(request.Bisnis_Process))
        {
            request.Bisnis_Process = "";
        }

        if (string.IsNullOrEmpty(request.Db_Server_Prod))
        {
            request.Db_Server_Prod = "";
        }

        if (string.IsNullOrEmpty(request.App_Server_Prod))
        {
            request.App_Server_Prod = "";
        }

        if (string.IsNullOrEmpty(request.Diagram_Context))
        {
            request.Diagram_Context = "";
        }

        if (string.IsNullOrEmpty(request.Application_Data))
        {
            request.Application_Data = "";
        }

        if (string.IsNullOrEmpty(request.Code_Apps_Update))
        {
            request.Code_Apps_Update = "";
        }

        if (string.IsNullOrEmpty(request.Diagram_Physical))
        {
            request.Diagram_Physical = "";
        }

        if (string.IsNullOrEmpty(request.Link_Application))
        {
            request.Link_Application = "";
        }

        if (string.IsNullOrEmpty(request.Capability_Level_1))
        {
            request.Capability_Level_1 = "";
        }

        if (string.IsNullOrEmpty(request.Capability_Level_2))
        {
            request.Capability_Level_2 = "";
        }

        if (string.IsNullOrEmpty(request.Application_License))
        {
            request.Application_License = "";
        }

        if (string.IsNullOrEmpty(request.Application_Package))
        {
            request.Application_Package = "";
        }

        if (string.IsNullOrEmpty(request.User_Manual_Document))
        {
            request.User_Manual_Document = "";
        }

        if (!string.IsNullOrEmpty(request.Created_Date.ToString()))
        {
            if (request.Created_Date.ToString().Contains("0001"))
            {
                request.Created_Date = System.DateTime.Now;
            }
        }
        else
        {
            request.Created_Date = System.DateTime.Now;
        }

        if (request.Tipe_Request == "Create")
        {
            var app = await _context.Data
         .Where(pp => pp.Application_Name == request.Application_Name)
    .AsNoTracking()
          .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);
            if (app.Any())
            {
                throw new AlreadyExistsExceptions("Data", nameof(request.Application_Name), request.Application_Name!);
            }
        }

        var codeapps = "";

        var apps = await _context.RequestData
         .AsNoTracking()
          .ProjectTo<GetSingleRequestData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);
        if (apps != null)
        {
            long convint = 0;
            foreach (var singledata in apps)
            {
                try
                {
                    var lastcodes = singledata.Code_Apps.Replace("CHK-", "");
                    var convlastcode = long.Parse(lastcodes);
                    if (convlastcode > convint)
                    {
                        convint = convlastcode;
                    }
                }
                catch (Exception ex)
                {
                    // Log and rethrow for any other unforeseen exceptions
                    var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                    //LogException(exceptionDetails);
                    //throw;  // Rethrow the exception to let the calling code handle it if needed
                }
            }

            convint += 1;
            codeapps = "CHK-" + convint.ToString();
        }

        var data = new RequestData
        {
            Id = 0,
            Tipe_Request = request.Tipe_Request,
            Company_Code = request.Company_Code,
            Code_Apps = codeapps,
            Application_Data = request.Application_Data,
            App_Server_Dev = request.App_Server_Dev,
            App_Server_Prod = request.App_Server_Prod,
            Code_Apps_Update = request.Code_Apps_Update,
            Keterangan = request.Keterangan,
            Application_Area = request.Application_Area,
            Application_Name = request.Application_Name,
            Application_Type = request.Application_Type,
            Description = request.Description,
            Diagram_Context = request.Diagram_Context,
            Diagram_Physical = request.Diagram_Physical,
            Capability_Level_1 = request.Capability_Level_1,
            Capability_Level_2 = request.Capability_Level_2,
            Bisnis_Process = request.Bisnis_Process,
            Utilization = request.Utilization,
            Application_Status = request.Application_Status,
            Application_License = request.Application_License,
            Application_Ats = request.Application_Ats,
            Application_Package = request.Application_Package,
            User_Management_Integration = request.User_Management_Integration,
            User_Manual_Document = request.User_Manual_Document,
            Business_Owner_Nama = request.Business_Owner_Nama,
            Business_Owner_Email = request.Business_Owner_Email,
            Business_Owner_Kbo = request.Business_Owner_KBO,
            Business_Owner_Jabatan = request.Business_Owner_Jabatan,
            Business_Owner_Pic = request.Business_Owner_PIC,
            Business_Owner_Pic_Email = request.Business_Owner_PIC_Email,
            Developer = request.Developer,
            Business_Analyst = request.Business_Analyst,
            Link_Application = request.Link_Application,
            Users = request.Users,
            Criticality = request.Criticality,
            Service_Owner = request.Service_Owner,
            Db_Server_Dev = request.Db_Server_Dev,
            Db_Name_Dev = request.Db_Name_Dev,
            Db_Name_Prod = request.Db_Name_Prod,
            Db_Server_Prod = request.Db_Server_Prod,
            IsApproved = request.IsApproved,
            Source = request.Source,
            Approved_Status = request.Approved_Status,
            CreatedBy = request.Created_By,
            Created = request.Created_Date,
            IsDeleted = request.IsDeleted,
        };
        try
        {
            data.Start_Development = request.Start_Development_Str;
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        try
        {
            data.Start_Implementation = request.Start_Implementation_Str;
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        try
        {
            data.Start_Rationalization = request.Start_Rationalization_Str;
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        try
        {
            data.Start_Sunset = request.Start_Sunset_Str;
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        await _context.RequestData.AddAsync(data, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        //var codeappsdrafthistorical = "";
        //var appsdrafthistorical = await _context.DraftHistoricalAppPhase
        // .AsNoTracking()
        //  .ProjectTo<GetSingleDraftHistoricalData>(_mapper.ConfigurationProvider)
        //.ToListAsync(cancellationToken);
        //if (appsdrafthistorical != null)
        //{
        //    long convint = 0;
        //    foreach (var singledata in appsdrafthistorical)
        //    {
        //        try
        //        {
        //            var lastcodes = singledata.Code_Apps.Replace("CHK-", "");
        //            var convlastcode = long.Parse(lastcodes);
        //            if (convlastcode > convint)
        //            {
        //                convint = convlastcode;
        //            }
        //        }
        //        catch
        //        {

        //        }
        //    }

        //    convint += 1;
        //    codeappsdrafthistorical = "CHK-" + convint.ToString();
        //}

        //var datadrafthistorical = new DraftHistoricalAppPhase
        //{
        //    Id = 0,
        //    Code_Apps = codeappsdrafthistorical,
        //    Code_Apps_Update = "",
        //    Name_Apps_Update = request.Application_Name,
        //    Phase = request.Application_Status,
        //    Date = request.Start_Implementation,
        //    Day = request.Start_Implementation.Day,
        //    Month = request.Start_Implementation.Month,
        //    Year = request.Start_Implementation.Year,
        //    IsApproved = request.IsApproved,
        //    Source = request.Source,
        //    Reason = request.Reason,
        //    Approved_Status = request.Approved_Status,
        //    CreatedBy = request.Created_By,
        //    Created = request.Created_Date,
        //    IsDeleted = request.IsDeleted,
        //};

        //await _context.DraftHistoricalAppPhase.AddAsync(datadrafthistorical, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        return new AddDraftCatalogResponse
        {
            Code_App = codeapps
        };
    }
}
