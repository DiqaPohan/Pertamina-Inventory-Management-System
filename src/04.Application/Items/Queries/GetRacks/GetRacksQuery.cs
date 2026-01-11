using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Shared.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Racks.Queries.GetRacks;

// DTO Sederhana untuk Dropdown
public record RackDto(string RackId, RackStatus Status);

public record GetRacksQuery : IRequest<List<RackDto>>;

public class GetRacksQueryHandler : IRequestHandler<GetRacksQuery, List<RackDto>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetRacksQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<List<RackDto>> Handle(GetRacksQuery request, CancellationToken cancellationToken)
    {
        // Ambil semua rak, urutkan berdasarkan ID biar rapi di dropdown
        return await _context.Racks
            .AsNoTracking()
            .OrderBy(r => r.RackId)
            .Select(r => new RackDto(r.RackId, r.Status))
            .ToListAsync(cancellationToken);
    }
}