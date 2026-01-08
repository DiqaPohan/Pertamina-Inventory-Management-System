using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItemById;

// Kita kirim Id, dan mengharapkan return berupa object Item (atau null)
public record GetItemByIdQuery(Guid Id) : IRequest<Item?>;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Item?>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetItemByIdQueryHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<Item?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}