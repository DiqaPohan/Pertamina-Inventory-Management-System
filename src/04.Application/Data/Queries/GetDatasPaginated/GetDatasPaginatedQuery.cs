using System.Reflection.Metadata;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Extensions;
using Pertamina.SolutionTemplate.Application.Common.Mappings;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Application.Data.Queries.GetDatasPaginated;

public class GetDatasPaginatedQuery : PaginatedListRequest, IRequest<PaginatedListResponse<GetSingleData>>
{
}

public class GetDatasPaginatedQueryValidator : AbstractValidator<GetDatasPaginatedQuery>
{
    public GetDatasPaginatedQueryValidator()
    {
        Include(new PaginatedListRequestValidator());
    }
}

public class GetDocumentsDocumentMapping : IMapFrom<Document, GetSingleData>
{
}

public class GetDatasPaginatedQueryHandler : IRequestHandler<GetDatasPaginatedQuery, PaginatedListResponse<GetSingleData>>
{
    private readonly ISolutionTemplateDbContext _context;
    private readonly IMapper _mapper;

    public GetDatasPaginatedQueryHandler(ISolutionTemplateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedListResponse<GetSingleData>> Handle(GetDatasPaginatedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Data.AsNoTracking();

        if (!string.IsNullOrEmpty(request.SearchText))
        {

            query = _context.Data
            .AsNoTracking().Where(x => x.Application_Name.Contains(request.SearchText));

            if (query.Count() <= 0)
            {
                query = _context.Data
            .AsNoTracking().Where(x => x.Description.Contains(request.SearchText));

                if (query.Count() <= 0)
                {
                    query = _context.Data
                .AsNoTracking().Where(x => x.Code_Apps.Contains(request.SearchText));

                }
            }
        }

        var result = await query
            .ProjectTo<GetSingleData>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.Page, request.PageSize, cancellationToken);

        return result.ToPaginatedListResponse();
    }
}
