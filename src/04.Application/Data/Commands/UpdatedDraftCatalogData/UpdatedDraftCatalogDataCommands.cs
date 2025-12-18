using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Attributes;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

namespace Pertamina.SolutionTemplate.Application.Data.Commands.UpdatedDraftCatalogData;
[Authorize(Policy = Permissions.KpiEnterprise_Catalog_Approval)]
public class UpdatedDraftCatalogDataCommands : AddDraftCatalogRequest, IRequest<AddDraftCatalogResponse>
{
}

public class UpdatedDraftCatalogDataCommandsValidator : AbstractValidator<UpdatedDraftCatalogDataCommands>
{
    public UpdatedDraftCatalogDataCommandsValidator()
    {
        Include(new AddDraftCatalogRequestValidator());
    }
}

public class UpdatedDraftCatalogDataCommandsHandler : IRequestHandler<UpdatedDraftCatalogDataCommands, AddDraftCatalogResponse>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IDateAndTimeService _dateTime;
    private readonly IMapper _mapper;

    public UpdatedDraftCatalogDataCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
    {

        //var config = new MapperConfiguration(cfg =>
        //{
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.RequestData, GetSingleRequestData>();
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
        //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
        //});
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.RequestData, GetSingleRequestData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.Data, GetSingleDataData>();
            //    cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _dateTime = dateTime;
        _mapper = mapper;
    }

    public async Task<AddDraftCatalogResponse> Handle(UpdatedDraftCatalogDataCommands request, CancellationToken cancellationToken)
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

        if (string.IsNullOrEmpty(request.Source))
        {
            request.Source = "";
        }

        if (string.IsNullOrEmpty(request.Reason))
        {
            request.Reason = "";
        }

        //bool bration = false;
        //if (!string.IsNullOrEmpty(request.Start_Rationalization.ToString()))
        //{
        //    if (request.Start_Rationalization.ToString().Contains("0001"))
        //    {
        //        bration = false;
        //    }
        //    else
        //    {
        //        bration = true;
        //    }
        //}

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

        var codeapps = request.Code_Apps;
        var app = await _context.RequestData
           .Where(pp => pp.Code_Apps == request.Code_Apps)
           .SingleOrDefaultAsync(cancellationToken);
        if (app is not null)
        {
            app.IsApproved = request.IsApproved;
            app.Approved_Status = request.Approved_Status;
            app.IsDeleted = request.IsDeleted;
            app.Reason = request.Reason;
            await _context.SaveChangesAsync(cancellationToken);

        }

        var codereturn = "";
        #region ApprovedData
        if (request.IsApproved == "Approved")
        {
            if (request.Tipe_Request == "Create")
            {
                #region CreateMasterData
                var codeappsmaster = "";
                var appsmaster = await _context.Data
               .AsNoTracking()
                .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
                if (appsmaster != null)
                {
                    long convint = 0;
                    foreach (var singledata in appsmaster)
                    {
                        try
                        {
                            var lastcodes = singledata.Code_Apps.Replace("RP-", "");
                            var convlastcode = long.Parse(lastcodes);
                            if (convlastcode > convint)
                            {
                                convint = convlastcode;
                            }
                        }
                        catch
                        {

                        }
                    }

                    convint += 1;
                    codeappsmaster = "RP-" + convint.ToString();
                }

                var datainsert = new Domain.Entities.Data
                {
                    Id = 0,
                    Code_Apps = codeappsmaster,
                    Application_Name = request.Application_Name,
                    Application_Area = request.Application_Area,
                    Application_Type = request.Application_Type,
                    Description = request.Description,
                    Application_Data = "",
                    Capability_Level_1 = "",
                    Capability_Level_2 = "",
                    Diagram_Context = "",
                    Diagram_Physical = "",
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
                    Business_Owner_KBO = request.Business_Owner_KBO,
                    Business_Owner_Jabatan = request.Business_Owner_Jabatan,
                    Business_Owner_PIC = request.Business_Owner_PIC,
                    Business_Owner_PIC_Email = request.Business_Owner_PIC_Email,
                    Developer = request.Developer,
                    Business_Analyst = request.Business_Analyst,
                    Link_Application = request.Link_Application,
                    Users = request.Users,
                    Start_Development = request.Start_Development.ToString(),
                    Start_Implementation = request.Start_Implementation.ToString(),
                    Criticality = request.Criticality,
                    Service_Owner = request.Service_Owner,
                    Db_Server_Dev = request.Db_Server_Dev,
                    Db_Name_Dev = request.Db_Name_Dev,
                    App_Server_Dev = request.App_Server_Dev,
                    Db_Server_Prod = request.Db_Server_Prod,
                    App_Server_Prod = request.App_Server_Prod,
                    Db_Name_Prod = request.Db_Name_Prod,
                    Keterangan = request.Keterangan,
                    Company_Code = request.Company_Code,
                    Created = request.Created_Date,
                    CreatedBy = request.Created_By,
                    ModifiedBy = request.Created_By,
                    Modified = request.Created_Date,
                    IsDeleted = request.IsDeleted,
                };

                await _context.Data.AddAsync(datainsert, cancellationToken);
                await _context.SaveChangesAsync(this, cancellationToken);

                #endregion
                codereturn = codeappsmaster;

                #region CreateDraftHistoricalMasterData
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
                //    Code_Apps_Update = codeappsmaster,
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

                //if (bration)
                //{
                //    datadrafthistorical.Phase = "Rasionalisasi";
                //}

                //await _context.DraftHistoricalAppPhase.AddAsync(datadrafthistorical, cancellationToken);
                //await _context.SaveChangesAsync(cancellationToken);

                #endregion

                #region CreateHistoricalMasterData
                //var datahistoricalinsert = new Domain.Entities.HistoricalAppPhase
                //{
                //    Id = 0,
                //    Code_Apps = codeappsmaster,
                //    Phase = request.Application_Status,
                //    Source = request.Source,
                //    Created = request.Created_Date,
                //    CreatedBy = request.Created_By,
                //    Keterangan = request.Keterangan,
                //    IsDeleted = request.IsDeleted,
                //    Date = request.Start_Implementation,
                //    Day = request.Start_Implementation.Day,
                //    Month = request.Start_Implementation.Month,
                //    Year = request.Start_Implementation.Year
                //};

                //if (bration)
                //{
                //    datadrafthistorical.Phase = "Rasionalisasi";
                //}

                //await _context.HistoricalAppPhase.AddAsync(datahistoricalinsert, cancellationToken);
                //await _context.SaveChangesAsync(this, cancellationToken);

                #endregion

            }
            else
            {
                // var boolphase = false;
                #region CreateMasterData
                var appsmaster = await _context.Data
                 .Where(pp => pp.Code_Apps == request.Code_Apps_Update)
           .SingleOrDefaultAsync(cancellationToken);
                if (appsmaster != null)
                {
                    if (appsmaster.Application_Status != request.Application_Status)
                    {
                        if (request.Application_Status == "")
                        {

                        }
                        else
                        {
                            //        boolphase = true;
                        }
                    }

                    appsmaster.Code_Apps = request.Code_Apps_Update;
                    appsmaster.Application_Name = request.Application_Name;
                    appsmaster.Application_Area = request.Application_Area;
                    appsmaster.Application_Type = request.Application_Type;
                    appsmaster.Description = request.Description;
                    appsmaster.Application_Data = "";
                    appsmaster.Capability_Level_1 = "";
                    appsmaster.Capability_Level_2 = "";
                    appsmaster.Diagram_Context = "";
                    appsmaster.Diagram_Physical = "";
                    appsmaster.Bisnis_Process = request.Bisnis_Process;
                    appsmaster.Utilization = request.Utilization;
                    appsmaster.Application_Status = request.Application_Status;
                    appsmaster.Application_License = request.Application_License;
                    appsmaster.Application_Ats = request.Application_Ats;
                    appsmaster.Application_Package = request.Application_Package;
                    appsmaster.User_Management_Integration = request.User_Management_Integration;
                    appsmaster.User_Manual_Document = request.User_Manual_Document;
                    appsmaster.Business_Owner_Nama = request.Business_Owner_Nama;
                    appsmaster.Business_Owner_Email = request.Business_Owner_Email;
                    appsmaster.Business_Owner_KBO = request.Business_Owner_KBO;
                    appsmaster.Business_Owner_Jabatan = request.Business_Owner_Jabatan;
                    appsmaster.Business_Owner_PIC = request.Business_Owner_PIC;
                    appsmaster.Business_Owner_PIC_Email = request.Business_Owner_PIC_Email;
                    appsmaster.Developer = request.Developer;
                    appsmaster.Business_Analyst = request.Business_Analyst;
                    appsmaster.Link_Application = request.Link_Application;
                    appsmaster.Users = request.Users;
                    appsmaster.Start_Development = request.Start_Development.ToString();
                    appsmaster.Start_Implementation = request.Start_Implementation.ToString();
                    appsmaster.Criticality = request.Criticality;
                    appsmaster.Service_Owner = request.Service_Owner;
                    appsmaster.Db_Server_Dev = request.Db_Server_Dev;
                    appsmaster.Db_Name_Dev = request.Db_Name_Dev;
                    appsmaster.App_Server_Dev = request.App_Server_Dev;
                    appsmaster.Db_Server_Prod = request.Db_Server_Prod;
                    appsmaster.App_Server_Prod = request.App_Server_Prod;
                    appsmaster.Db_Name_Prod = request.Db_Name_Prod;
                    appsmaster.Keterangan = request.Keterangan;
                    appsmaster.Company_Code = request.Company_Code;
                    appsmaster.Created = request.Created_Date;
                    appsmaster.CreatedBy = request.Created_By;
                    appsmaster.ModifiedBy = request.Created_By;
                    appsmaster.Modified = request.Created_Date;
                    appsmaster.IsDeleted = request.IsDeleted;

                    await _context.SaveChangesAsync(this, cancellationToken);
                }
                #endregion

                #region CreateHistorical
                //if (boolphase)
                //{
                //    #region CreateDraftHistoricalMasterData
                //    var codeappsdrafthistorical = "";
                //    var appsdrafthistorical = await _context.DraftHistoricalAppPhase
                //     .AsNoTracking()
                //      .ProjectTo<GetSingleDraftHistoricalData>(_mapper.ConfigurationProvider)
                //    .ToListAsync(cancellationToken);
                //    if (appsdrafthistorical != null)
                //    {
                //        long convint = 0;
                //        foreach (var singledata in appsdrafthistorical)
                //        {
                //            try
                //            {
                //                var lastcodes = singledata.Code_Apps.Replace("CHK-", "");
                //                var convlastcode = long.Parse(lastcodes);
                //                if (convlastcode > convint)
                //                {
                //                    convint = convlastcode;
                //                }
                //            }
                //            catch
                //            {

                //            }
                //        }

                //        convint += 1;
                //        codeappsdrafthistorical = "CHK-" + convint.ToString();
                //    }

                //    var datadrafthistorical = new DraftHistoricalAppPhase
                //    {
                //        Id = 0,
                //        Code_Apps = codeappsdrafthistorical,
                //        Code_Apps_Update = request.Code_Apps_Update,
                //        Name_Apps_Update = request.Application_Name,
                //        Phase = request.Application_Status,
                //        Date = request.Start_Implementation,
                //        Day = request.Start_Implementation.Day,
                //        Month = request.Start_Implementation.Month,
                //        Year = request.Start_Implementation.Year,
                //        IsApproved = request.IsApproved,
                //        Source = request.Source,
                //        Reason = request.Reason,
                //        Approved_Status = request.Approved_Status,
                //        CreatedBy = request.Created_By,
                //        Created = request.Created_Date,
                //        IsDeleted = request.IsDeleted,
                //    };

                //    if (bration)
                //    {
                //        datadrafthistorical.Phase = "Rasionalisasi";
                //    }

                //    await _context.DraftHistoricalAppPhase.AddAsync(datadrafthistorical, cancellationToken);
                //    await _context.SaveChangesAsync(cancellationToken);
                //    #endregion

                //    #region CreateHistoricalMasterData
                //    var datahistoricalinsert = new Domain.Entities.HistoricalAppPhase
                //    {
                //        Id = 0,
                //        Code_Apps = request.Code_Apps_Update,
                //        Phase = request.Application_Status,
                //        Source = request.Source,
                //        Created = request.Created_Date,
                //        CreatedBy = request.Created_By,
                //        Keterangan = request.Keterangan,
                //        IsDeleted = request.IsDeleted,
                //        Date = request.Start_Implementation,
                //        Day = request.Start_Implementation.Day,
                //        Month = request.Start_Implementation.Month,
                //        Year = request.Start_Implementation.Year
                //    };

                //    if (bration)
                //    {
                //        datadrafthistorical.Phase = "Rasionalisasi";
                //    }

                //    await _context.HistoricalAppPhase.AddAsync(datahistoricalinsert, cancellationToken);
                //    await _context.SaveChangesAsync(this, cancellationToken);
                //    #endregion
                //}

                #endregion
                codereturn = request.Code_Apps_Update;
            }

        }

        #endregion

        return new AddDraftCatalogResponse
        {
            Code_App = codereturn
        };
    }
}
