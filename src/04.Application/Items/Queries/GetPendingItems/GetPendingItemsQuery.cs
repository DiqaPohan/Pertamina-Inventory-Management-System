using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Shared.Common.Enums;
using System.Linq;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetPendingItems;

// DTO diupdate biar makin informatif buat pegawai
public record PendingItemDto(
    Guid Id,
    string Name,
    string RackId,
    ItemStatus Status,
    RackStatus? TargetRackStatus // Tambahin ini biar ketauan rak tujuannya penuh apa kagak
);

public record GetPendingItemsQuery : IRequest<List<PendingItemDto>>;

public class GetPendingItemsQueryHandler : IRequestHandler<GetPendingItemsQuery, List<PendingItemDto>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetPendingItemsQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<List<PendingItemDto>> Handle(GetPendingItemsQuery request, CancellationToken cancellationToken)
    {
        // Ambil barang yang statusnya masih Pending dan BELUM dihapus
        var query = await _context.Items
            .AsNoTracking() // Biar enteng karena cuma buat baca doang
            .Include(x => x.Rack) // Ambil info tabel Racks (JOIN)
            .Where(x => x.Status == ItemStatus.Pending && !x.IsDeleted)
            .Select(x => new PendingItemDto(
                x.Id,
                x.Name,
                x.RackId,
                x.Status,
                x.Rack != null ? x.Rack.Status : null // Ambil status rak dari tabel Racks
            ))
            .ToListAsync(cancellationToken);

        return query;
    }
}