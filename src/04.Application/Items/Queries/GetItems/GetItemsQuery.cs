using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItems;

public class GetItemsQuery : PaginatedListRequest, IRequest<PaginatedListResponse<Item>>
{
    public string? RackId { get; set; }
    public ItemStatus? Status { get; set; }
}

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PaginatedListResponse<Item>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetItemsQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedListResponse<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        // 1. Query dasar dengan Include Rack (Eager Loading)
        // Kita tarik data Rack-nya sekalian biar relasi FK-nya kepake
        var query = _context.Items
            .Include(x => x.Rack)
            .AsNoTracking();

        // 2. Filter IsDeleted (Standard Practice)
        query = query.Where(x => !x.IsDeleted);

        // 3. LOGIC FILTER STATUS
        // Jika request status tidak diisi, secara default kita CUMA nampilin yang Active
        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }
        else
        {
            query = query.Where(x => x.Status == ItemStatus.Active);
        }

        // 4. Filter berdasarkan RackId
        if (!string.IsNullOrEmpty(request.RackId))
        {
            query = query.Where(x => x.RackId == request.RackId);
        }

        // 5. Hitung total data sebelum paging
        var totalCount = await query.CountAsync(cancellationToken);

        // 6. Ambil data dengan Paging & Sorting
        var items = await query
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedListResponse<Item>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}