using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItems;

public record GetItemsQuery : IRequest<List<Item>>;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<Item>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetItemsQueryHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<List<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking() // Bagus untuk performa karena hanya membaca data
            .ToListAsync(cancellationToken);
    }
}