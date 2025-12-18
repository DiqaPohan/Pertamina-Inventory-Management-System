using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleMasterData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetMasterDataByTokenData;

namespace Pertamina.SolutionTemplate.Application.Public.Queries.GetTableMasterData;
public class GetTableMasterDataQuery : IRequest<OutputGetMasterDataByTokenData>
{
    public string AppValue { get; set; }
}

public class GetTableMasterDataQueryHandler : IRequestHandler<GetTableMasterDataQuery, OutputGetMasterDataByTokenData>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetTableMasterDataQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationArea, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel1, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel2, ApplicationCapabilityLevel2>();
            //cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel2, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCriticality, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationLicense, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationPackage, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationStatus, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationType, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUserManagement, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUtilization, GetSingleMasterDataData>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _mapper = mapper;
    }
    public async Task<OutputGetMasterDataByTokenData> Handle(GetTableMasterDataQuery request, CancellationToken cancellationToken)
    {
        var output = new OutputGetMasterDataByTokenData
        {
            Items = new List<GetSingleMasterDataData>()
        };
        if (!string.IsNullOrEmpty(request.AppValue))
        {
            if (request.AppValue == "area")
            {
                //area
                try
                {
                    var appsarea = await _context.ApplicationArea
                        .AsNoTracking()
                   .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                    if (appsarea.Count > 0)
                    {
                        foreach (var itemapps in appsarea)
                        {
                            itemapps.Table_Name = "Area";
                        }

                        output.Items.AddRange(appsarea.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "capabilitylevel1")
            {
                //capability level1
                try
                {
                    var appscapabilitylevel1 = await _context.ApplicationCapabilityLevel1
               .AsNoTracking()
          .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
                    if (appscapabilitylevel1.Count > 0)
                    {
                        foreach (var itemapps in appscapabilitylevel1)
                        {
                            itemapps.Table_Name = "Capability Level 1";
                        }

                        output.Items.AddRange(appscapabilitylevel1.ToList());
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (request.AppValue == "capabilitylevel2")
            {
                //capability level2
                try
                {
                    var appscapabilitylevel2 = await _context.ApplicationCapabilityLevel2
           .AsNoTracking()
        .ProjectTo<ApplicationCapabilityLevel2>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);
                    if (appscapabilitylevel2.Count > 0)
                    {
                        var listdata = new List<GetSingleMasterDataData>();
                        var appscapabilitylevel1 = await _context.ApplicationCapabilityLevel1
             .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
         .ToListAsync(cancellationToken);
                        if (appscapabilitylevel1.Count > 0)
                        {
                            foreach (var itemapps in appscapabilitylevel2)
                            {
                                try
                                {
                                    itemapps.Level_1 = appscapabilitylevel1.Where(pp => pp.Id == itemapps.Level_1).SingleOrDefault().Nama;
                                }
                                catch (Exception ex)
                                {

                                }

                                var itemdata = new GetSingleMasterDataData
                                {
                                    Id = itemapps.Id,
                                    Nama = itemapps.Nama + " (" + itemapps.Level_1 + ")",
                                    Keterangan = itemapps.Keterangan,
                                    Table_Name = "Capability Level 2"
                                };
                                listdata.Add(itemdata);
                            }

                            output.Items.AddRange(listdata.ToList());
                        }
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "utilization")
            {
                //utilization
                try
                {

                    var appstype = await _context.ApplicationUtilization
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "Utilization";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "status")
            {
                //status
                try
                {

                    var appstype = await _context.ApplicationStatus
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "Status";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "license")
            {
                //license
                try
                {
                    var appstype = await _context.ApplicationLicense
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "License";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "package")
            {
                //package
                try
                {

                    var appstype = await _context.ApplicationPackage
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "Package";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (request.AppValue == "user management")
            {
                //user management
                try
                {

                    var appstype = await _context.ApplicationUserManagement
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "User Management";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (request.AppValue == "criticality")
            {
                //criticality
                try
                {

                    var appstype = await _context.ApplicationCriticality
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "Criticality";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (request.AppValue == "type")
            {

                //type
                try
                {

                    var appstype = await _context.ApplicationType
           .AsNoTracking()
        .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

                    if (appstype.Count > 0)
                    {
                        foreach (var itemapps in appstype)
                        {
                            itemapps.Table_Name = "Type";
                        }

                        output.Items.AddRange(appstype.ToList());
                    }
                }
                catch (Exception ex)
                {
                }

            }
        }
        else
        {
            //area
            try
            {
                var appsarea = await _context.ApplicationArea
                    .AsNoTracking()
               .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                if (appsarea.Count > 0)
                {
                    foreach (var itemapps in appsarea)
                    {
                        itemapps.Table_Name = "Area";
                    }

                    output.Items.AddRange(appsarea.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //capability level1
            try
            {
                var appscapabilitylevel1 = await _context.ApplicationCapabilityLevel1
           .AsNoTracking()
      .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
       .ToListAsync(cancellationToken);
                if (appscapabilitylevel1.Count > 0)
                {
                    foreach (var itemapps in appscapabilitylevel1)
                    {
                        itemapps.Table_Name = "Capability Level 1";
                    }

                    output.Items.AddRange(appscapabilitylevel1.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //capability level2
            try
            {
                var appscapabilitylevel2 = await _context.ApplicationCapabilityLevel2
       .AsNoTracking()
    .ProjectTo<ApplicationCapabilityLevel2>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);
                if (appscapabilitylevel2.Count > 0)
                {
                    var listdata = new List<GetSingleMasterDataData>();
                    var appscapabilitylevel1 = await _context.ApplicationCapabilityLevel1
         .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
     .ToListAsync(cancellationToken);
                    if (appscapabilitylevel1.Count > 0)
                    {
                        foreach (var itemapps in appscapabilitylevel2)
                        {
                            try
                            {
                                itemapps.Level_1 = appscapabilitylevel1.Where(pp => pp.Id == itemapps.Level_1).SingleOrDefault().Nama;
                            }
                            catch (Exception ex)
                            {

                            }

                            var itemdata = new GetSingleMasterDataData
                            {
                                Id = itemapps.Id,
                                Nama = itemapps.Nama + " (" + itemapps.Level_1 + ")",
                                Keterangan = itemapps.Keterangan,
                                Table_Name = "Capability Level 2"
                            };
                            listdata.Add(itemdata);
                        }

                        output.Items.AddRange(listdata.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //utilization
            try
            {

                var appstype = await _context.ApplicationUtilization
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Utilization";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //status
            try
            {

                var appstype = await _context.ApplicationStatus
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Status";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //license
            try
            {
                var appstype = await _context.ApplicationLicense
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "License";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //package
            try
            {

                var appstype = await _context.ApplicationPackage
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Package";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //user management
            try
            {

                var appstype = await _context.ApplicationUserManagement
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "User Management";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //criticality
            try
            {

                var appstype = await _context.ApplicationCriticality
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Criticality";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            //type
            try
            {

                var appstype = await _context.ApplicationType
       .AsNoTracking()
    .ProjectTo<GetSingleMasterDataData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Type";
                    }

                    output.Items.AddRange(appstype.ToList());
                }
            }
            catch (Exception ex)
            {
            }

        }

        if (output.Items.Count > 0)
        {
            output.ResponseCode = "S";
            output.ResponseMessage = "Sukses";
        }
        else
        {
            output.ResponseCode = "S";
            output.ResponseMessage = "Master data kosong";
        }

        output.Tanggal = System.DateTime.Now;

        return output;
    }
}

