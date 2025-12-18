using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataStringByToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.UpdateDataWithToken;

namespace Pertamina.SolutionTemplate.Application.Public.Commands.UpdatedCatalogData;
public class UpdatedCatalogDataCommands : UpdateDataWithToken, IRequest<OutputGetDataStringByTokenData>
{
}

public class UpdatedCatalogDataCommandsValidator : AbstractValidator<UpdatedCatalogDataCommands>
{
    //public CreatedCatalogDataCommandsValidator()
    //{
    //    // Include(new CreateTicketRequestValidator());
    //}
}
public class UpdatedCatalogDataCommandsResponseMapping : IMapFrom<Domain.Entities.Data, Domain.Entities.Data>
{
}
public class UpdatedCatalogDataCommandsHandler : IRequestHandler<UpdatedCatalogDataCommands, OutputGetDataStringByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IDateAndTimeService _dateTime;
    private readonly IMapper _mapper;
    public UpdatedCatalogDataCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
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
            //      cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
            //      cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _dateTime = dateTime;
        _mapper = mapper;
    }

    public async Task<OutputGetDataStringByTokenData> Handle(UpdatedCatalogDataCommands request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataStringByTokenData();
        try
        {
            var boolphase = false;
            var app = await _context.Data
           .Where(pp => pp.Code_Apps == request.Code_Apps)
           .SingleOrDefaultAsync(cancellationToken);
            if (app is null)
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "application id tidak di temukan, gunakan data yang lain";
                output.Tanggal = System.DateTime.Now;
                output.Items = "";
            }
            else
            {
                if (app.Application_Status != request.Application_Status)
                {
                    if (request.Application_Status == "")
                    {

                    }
                    else
                    {
                        boolphase = true;
                    }
                }

                app.Code_Apps = request.Code_Apps;
                app.Application_Name = request.Application_Name;
                app.Application_Area = request.Application_Area;
                app.Application_Type = request.Application_Type;
                app.Description = request.Description;
                app.Bisnis_Process = request.Bisnis_Process;
                app.Application_Status = request.Application_Status;
                app.User_Management_Integration = request.User_Management_Integration;
                app.Business_Owner_Nama = request.Business_Owner_Nama;
                app.Business_Owner_Email = request.Business_Owner_Email;
                app.Business_Owner_KBO = request.Business_Owner_KBO;
                app.Business_Owner_Jabatan = request.Business_Owner_Jabatan;
                app.Business_Owner_PIC = request.Business_Owner_PIC;
                app.Business_Owner_PIC_Email = request.Business_Owner_PIC_Email;
                app.Developer = request.Developer;
                app.Business_Analyst = request.Business_Analyst;
                app.Start_Development = request.Start_Development.ToString();
                app.Start_Implementation = request.Start_Implementation.ToString();
                app.Modified = request.Updated_Date;
                app.ModifiedBy = request.Updated_By;
                app.Utilization = request.Utilization;
                app.Application_License = request.Application_License;
                app.Application_Ats = request.Application_Ats;
                app.Application_Package = request.Application_Package;
                app.User_Manual_Document = request.User_Manual_Document;
                app.Link_Application = request.Link_Application;
                app.Users = request.Users;
                app.Criticality = request.Criticality;
                app.Service_Owner = request.Service_Owner;
                app.Db_Server_Dev = request.Db_Server_Dev;
                app.Db_Name_Dev = request.Db_Name_Dev;
                app.App_Server_Dev = request.App_Server_Dev;
                app.Db_Server_Prod = request.Db_Server_Prod;
                app.App_Server_Prod = request.App_Server_Prod;
                app.Db_Name_Prod = request.Db_Name_Prod;
                app.Keterangan = request.Keterangan;
                app.Company_Code = request.Company_Code;

                await _context.SaveChangesAsync(this, cancellationToken);
                #region CreateDataDraft
                var codeappsdraft = "";
                var appsrequest = await _context.RequestData
                 .AsNoTracking()
                  .ProjectTo<GetSingleRequestData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                if (appsrequest != null)
                {
                    long convint = 0;
                    foreach (var singledata in appsrequest)
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

                        }
                    }

                    convint += 1;
                    codeappsdraft = "CHK-" + convint.ToString();
                }

                var data = new RequestData
                {
                    Id = 0,
                    Tipe_Request = "Update",
                    Company_Code = app.Company_Code,
                    Code_Apps = codeappsdraft,
                    Application_Data = app.Application_Data,
                    App_Server_Dev = app.App_Server_Dev,
                    App_Server_Prod = app.App_Server_Prod,
                    Code_Apps_Update = app.Code_Apps,
                    Keterangan = app.Keterangan,
                    Application_Area = app.Application_Area,
                    Application_Name = app.Application_Name,
                    Application_Type = app.Application_Type,
                    Description = app.Description,
                    Diagram_Context = app.Diagram_Context,
                    Diagram_Physical = app.Diagram_Physical,
                    Capability_Level_1 = app.Capability_Level_1,
                    Capability_Level_2 = app.Capability_Level_2,
                    Bisnis_Process = app.Bisnis_Process,
                    Utilization = app.Utilization,
                    Application_Status = app.Application_Status,
                    Application_License = app.Application_License,
                    Application_Ats = app.Application_Ats,
                    Application_Package = app.Application_Package,
                    User_Management_Integration = app.User_Management_Integration,
                    User_Manual_Document = app.User_Manual_Document,
                    Business_Owner_Nama = app.Business_Owner_Nama,
                    Business_Owner_Email = app.Business_Owner_Email,
                    Business_Owner_Kbo = app.Business_Owner_KBO,
                    Business_Owner_Jabatan = app.Business_Owner_Jabatan,
                    Business_Owner_Pic = app.Business_Owner_PIC,
                    Business_Owner_Pic_Email = app.Business_Owner_PIC_Email,
                    Developer = app.Developer,
                    Business_Analyst = app.Business_Analyst,
                    Link_Application = app.Link_Application,
                    Users = app.Users,
                    Criticality = app.Criticality,
                    Service_Owner = app.Service_Owner,
                    Db_Server_Dev = app.Db_Server_Dev,
                    Db_Name_Dev = app.Db_Name_Dev,
                    Db_Name_Prod = app.Db_Name_Prod,
                    Db_Server_Prod = app.Db_Server_Prod,
                    IsApproved = "Approved",
                    Source = "Api",
                    Reason = "",
                    Approved_Status = "Approved",
                    CreatedBy = app.CreatedBy,
                    Created = app.Created,
                    IsDeleted = app.IsDeleted
                };
                try
                {
                    data.Start_Development = app.Start_Development;
                }
                catch (Exception ex)
                {

                }

                try
                {
                    data.Start_Implementation = app.Start_Implementation;
                }
                catch (Exception ex)
                {

                }

                await _context.RequestData.AddAsync(data, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                #endregion

                if (boolphase)
                {
                    #region CreateDataDraftHistorical

                    //var codeappsdrafthisto = "";
                    //var appsdrafthist = await _context.DraftHistoricalAppPhase
                    // .AsNoTracking()
                    //  .ProjectTo<GetSingleDraftHistoricalData>(_mapper.ConfigurationProvider)
                    //.ToListAsync(cancellationToken);
                    //if (appsdrafthist != null)
                    //{
                    //    long convint = 0;
                    //    foreach (var singledata in appsdrafthist)
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
                    //    codeappsdrafthisto = "CHK-" + convint.ToString();
                    //}

                    //var datadrafthist = new DraftHistoricalAppPhase
                    //{
                    //    Id = 0,
                    //    Code_Apps = codeappsdrafthisto,
                    //    Code_Apps_Update = request.Code_Apps,
                    //    Name_Apps_Update = request.Application_Name,
                    //    Phase = request.Application_Status,
                    //    IsDeleted = app.IsDeleted,
                    //    IsApproved = "Approved",
                    //    Source = "Web CRUD",
                    //    Reason = "",
                    //    Approved_Status = "Approved",
                    //    CreatedBy = app.CreatedBy,
                    //    Created = app.Created,
                    //    Date = request.Start_Implementation,
                    //    Day = request.Start_Implementation.Day,
                    //    Month = request.Start_Implementation.Month,
                    //    Year = request.Start_Implementation.Year
                    //};
                    //await _context.DraftHistoricalAppPhase.AddAsync(datadrafthist, cancellationToken);
                    //await _context.SaveChangesAsync(cancellationToken);
                    #endregion
                    #region CreateRealDataHistorical
                    //var datahist = new HistoricalAppPhase
                    //{
                    //    Id = 0,
                    //    Code_Apps = app.Code_Apps,
                    //    Phase = app.Application_Status,
                    //    Source = "Api",
                    //    CreatedBy = app.CreatedBy,
                    //    Created = app.Created,
                    //    IsDeleted = "false",
                    //    Date = request.Start_Implementation,
                    //    Day = request.Start_Implementation.Day,
                    //    Month = request.Start_Implementation.Month,
                    //    Year = request.Start_Implementation.Year
                    //};
                    //await _context.HistoricalAppPhase.AddAsync(datahist, cancellationToken);
                    //await _context.SaveChangesAsync(cancellationToken);
                    #endregion

                }

                output.ResponseCode = "S";
                output.ResponseMessage = "Sukses";
                output.Items = app.Code_Apps;
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
            output.Items = "";
        }

        return output;
    }
}
