using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetAllMasterData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetTableMasterData;
public class GetTableMasterDataQuery : IRequest<ListResponse<GetAllMasterData>>
{
    public string AppValue { get; set; }
}

public class GetTableMasterDataQueryHandler : IRequestHandler<GetTableMasterDataQuery, ListResponse<GetAllMasterData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;
    public GetTableMasterDataQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationArea, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel1, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel2, GetAllMasterData>();
            //cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel2, GetSingleMasterDataData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCriticality, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationLicense, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationPackage, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationStatus, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationType, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUserManagement, GetAllMasterData>();
            cfg.CreateMap<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUtilization, GetAllMasterData>();
        });
        mapper = config.CreateMapper();
        _context = context;
        _mapper = mapper;
    }
    public async Task<ListResponse<GetAllMasterData>> Handle(GetTableMasterDataQuery request, CancellationToken cancellationToken)
    {
        var output = new ListResponse<GetAllMasterData>
        {
            Items = new List<GetAllMasterData>()
        };
        if (!string.IsNullOrEmpty(request.AppValue))
        {

        }
        else
        {
            //area
            try
            {
                var appsarea = await _context.ApplicationArea
                    .AsNoTracking()
               .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                if (appsarea.Count > 0)
                {
                    foreach (var itemapps in appsarea)
                    {
                        itemapps.Table_Name = "Area";
                        output.Items.Add(itemapps);
                    }
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
      .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
       .ToListAsync(cancellationToken);
                if (appscapabilitylevel1.Count > 0)
                {
                    foreach (var itemapps in appscapabilitylevel1)
                    {
                        itemapps.Table_Name = "Capability Level 1";
                        output.Items.Add(itemapps);
                    }
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
                    var listdata = new List<GetAllMasterData>();
                    var appscapabilitylevel1 = await _context.ApplicationCapabilityLevel1
         .AsNoTracking()
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
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

                            var itemdata = new GetAllMasterData
                            {
                                Id = itemapps.Id,
                                Nama = itemapps.Nama + " (" + itemapps.Level_1 + ")",
                                Keterangan = itemapps.Keterangan,
                                Table_Name = "Capability Level 2"
                            };
                            listdata.Add(itemdata);
                            output.Items.Add(itemdata);
                        }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Utilization";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Status";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "License";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Package";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "User Management";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Criticality";
                        output.Items.Add(itemapps);
                    }
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
    .ProjectTo<GetAllMasterData>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);

                if (appstype.Count > 0)
                {
                    foreach (var itemapps in appstype)
                    {
                        itemapps.Table_Name = "Type";
                        output.Items.Add(itemapps);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        return output;
    }
}

