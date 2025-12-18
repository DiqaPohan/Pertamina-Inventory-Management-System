using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.InsertDataWithToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataStringByToken;

namespace Pertamina.SolutionTemplate.Application.Public.Commands.CreatedCatalogData;
public class CreatedCatalogDataCommands : InsertDataWithToken, IRequest<OutputGetDataStringByTokenData>
{
}

public class CreatedCatalogDataCommandsValidator : AbstractValidator<CreatedCatalogDataCommands>
{
    //public CreatedCatalogDataCommandsValidator()
    //{
    //    // Include(new CreateTicketRequestValidator());
    //}
}

public class CreatedCatalogDataCommandsHandler : IRequestHandler<CreatedCatalogDataCommands, OutputGetDataStringByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IDateAndTimeService _dateTime;
    private readonly IMapper _mapper;
    public CreatedCatalogDataCommandsHandler(ISolutionTemplateDbContext context, IDateAndTimeService dateTime, IMapper mapper)
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
            //   cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.HistoricalAppPhase, GetSingleDataHistoricalApplicationPhase>();
            // cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.DraftHistoricalAppPhase, GetSingleDraftHistoricalData>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _dateTime = dateTime;
        _mapper = mapper;
    }

    public async Task<OutputGetDataStringByTokenData> Handle(CreatedCatalogDataCommands request, CancellationToken cancellationToken)
    {
        var output = new OutputGetDataStringByTokenData();
        try
        {
            var bapp = false;
            var codeapps = "";
            var apps = await _context.Data
           .AsNoTracking()
            .ProjectTo<GetSingleDataData>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
            if (apps != null)
            {
                long convint = 0;
                foreach (var singledata in apps)
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
                    catch (Exception ex)
                    {
                        // Log and rethrow for any other unforeseen exceptions
                        var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                        //LogException(exceptionDetails);
                        //throw;  // Rethrow the exception to let the calling code handle it if needed
                    }
                }

                convint += 1;
                codeapps = "RP-" + convint.ToString();

                var app = apps.Where(pp => pp.Application_Name == request.Application_Name).SingleOrDefault();
                if (app != null)
                {
                    bapp = true;
                }
            }

            if (bapp)
            {
                output.ResponseCode = "E";
                output.ResponseMessage = "application name di temukan, gunakan data yang lain";
                output.Tanggal = System.DateTime.Now;
                output.Items = "";
            }
            else
            {

                var datainsert = new Domain.Entities.Data
                {
                    Id = 0,
                    Code_Apps = codeapps,
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
                    IsDeleted = "false"
                };

                await _context.Data.AddAsync(datainsert, cancellationToken);
                await _context.SaveChangesAsync(this, cancellationToken);
                #region CreateRealHistorical
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
                            // Log and rethrow for any other unforeseen exceptions
                            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                            //LogException(exceptionDetails);
                            //throw;  // Rethrow the exception to let the calling code handle it if needed
                        }
                    }

                    convint += 1;
                    codeappsdraft = "CHK-" + convint.ToString();
                }

                var data = new RequestData
                {
                    Id = 0,
                    Tipe_Request = "Create",
                    Company_Code = datainsert.Company_Code,
                    Code_Apps = codeappsdraft,
                    Application_Data = datainsert.Application_Data,
                    App_Server_Dev = datainsert.App_Server_Dev,
                    App_Server_Prod = datainsert.App_Server_Prod,
                    Code_Apps_Update = datainsert.Code_Apps,
                    Keterangan = datainsert.Keterangan,
                    Application_Area = datainsert.Application_Area,
                    Application_Name = datainsert.Application_Name,
                    Application_Type = datainsert.Application_Type,
                    Description = datainsert.Description,
                    Diagram_Context = datainsert.Diagram_Context,
                    Diagram_Physical = datainsert.Diagram_Physical,
                    Capability_Level_1 = datainsert.Capability_Level_1,
                    Capability_Level_2 = datainsert.Capability_Level_2,
                    Bisnis_Process = datainsert.Bisnis_Process,
                    Utilization = datainsert.Utilization,
                    Application_Status = datainsert.Application_Status,
                    Application_License = datainsert.Application_License,
                    Application_Ats = datainsert.Application_Ats,
                    Application_Package = datainsert.Application_Package,
                    User_Management_Integration = datainsert.User_Management_Integration,
                    User_Manual_Document = datainsert.User_Manual_Document,
                    Business_Owner_Nama = datainsert.Business_Owner_Nama,
                    Business_Owner_Email = datainsert.Business_Owner_Email,
                    Business_Owner_Kbo = datainsert.Business_Owner_KBO,
                    Business_Owner_Jabatan = datainsert.Business_Owner_Jabatan,
                    Business_Owner_Pic = datainsert.Business_Owner_PIC,
                    Business_Owner_Pic_Email = datainsert.Business_Owner_PIC_Email,
                    Developer = datainsert.Developer,
                    Business_Analyst = datainsert.Business_Analyst,
                    Link_Application = datainsert.Link_Application,
                    Users = datainsert.Users,
                    Criticality = datainsert.Criticality,
                    Service_Owner = datainsert.Service_Owner,
                    Db_Server_Dev = datainsert.Db_Server_Dev,
                    Db_Name_Dev = datainsert.Db_Name_Dev,
                    Db_Name_Prod = datainsert.Db_Name_Prod,
                    Db_Server_Prod = datainsert.Db_Server_Prod,
                    IsApproved = "Approved",
                    Source = "Api",
                    Approved_Status = "Approved",
                    CreatedBy = request.Created_By,
                    Created = request.Created_Date,
                    Reason = "",
                    IsDeleted = "false",
                };
                try
                {
                    data.Start_Development = datainsert.Start_Development;
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
                    data.Start_Implementation = datainsert.Start_Implementation;
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
                #endregion
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
                //    Code_Apps_Update = codeapps,
                //    Name_Apps_Update = request.Application_Name,
                //    Phase = request.Application_Status,
                //    IsDeleted = "false",
                //    IsApproved = "Approved",
                //    Source = "Api",
                //    Approved_Status = "Approved",
                //    Reason = "",
                //    CreatedBy = request.Created_By,
                //    Created = request.Created_Date,
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
                //    Code_Apps = codeapps,
                //    Phase = datainsert.Application_Status,
                //    Source = "Api",
                //    CreatedBy = request.Created_By,
                //    Created = request.Created_Date,
                //    IsDeleted = "false",
                //    Date = request.Start_Implementation,
                //    Day = request.Start_Implementation.Day,
                //    Month = request.Start_Implementation.Month,
                //    Year = request.Start_Implementation.Year
                //};
                //await _context.HistoricalAppPhase.AddAsync(datahist, cancellationToken);
                //await _context.SaveChangesAsync(cancellationToken);
                #endregion
                #endregion
                output.ResponseCode = "S";
                output.ResponseMessage = "Sukses";
                output.Items = datainsert.Code_Apps;
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
