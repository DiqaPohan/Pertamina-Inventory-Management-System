using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetPendingItems;

// DTO untuk membungkus data yang dikirim ke Front-End
public record PendingItemDto(
    Guid Id,
    string Name,
    string RackId,
    ItemStatus Status
);

// Query utama
public record GetPendingItemsQuery : IRequest<List<PendingItemDto>>;

// Handler untuk memproses pengambilan data dari database
public class GetPendingItemsQueryHandler : IRequestHandler<GetPendingItemsQuery, List<PendingItemDto>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetPendingItemsQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<List<PendingItemDto>> Handle(GetPendingItemsQuery request, CancellationToken cancellationToken)
    {
        // Ambil barang yang statusnya masih Pending
        var query = await _context.Items
            .Where(x => x.Status == ItemStatus.Pending)
            .Select(x => new PendingItemDto(
                x.Id,
                x.Name,
                x.RackId, // RackId yang sudah diisi Admin sebelumnya
                x.Status
            ))
            .ToListAsync(cancellationToken);

        return query;
    }
}