using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItemById;

public record GetItemByIdQuery(Guid Id) : IRequest<Item?>;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Item?>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetItemByIdQueryHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<Item?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking()
            .Include(x => x.Rack) // WAJIB: Biar data Rak-nya ke-load (Eager Loading)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // Catatan: Filter .Where(Status == Active) dihapus supaya Admin 
        // tetep bisa liat detail barang meskipun statusnya masih Pending.
    }
}