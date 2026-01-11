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
        // 1. Ambil data dasar dan include Rack-nya
        var query = _context.Items
            .Include(x => x.Rack)
            .Where(x => !x.IsDeleted) // Jangan tampilin yang sudah dihapus
            .AsNoTracking();

        // 2. FILTER STATUS (DIPERBAIKI)
        // Jika user minta status spesifik (misal cuma mau liat yang Pending), baru kita filter.
        // Jika tidak diisi (null), kita tampilin SEMUANYA (Active & Pending).
        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        // 3. Filter berdasarkan RackId jika ada
        if (!string.IsNullOrEmpty(request.RackId))
        {
            query = query.Where(x => x.RackId == request.RackId);
        }

        // 4. Hitung total data
        var totalCount = await query.CountAsync(cancellationToken);

        // 5. Eksekusi Query dengan Paging & Sorting terbaru
        var items = await query
            .OrderByDescending(x => x.Created) // Tampilin yang paling baru lu input di atas
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