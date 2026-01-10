using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<Guid>
{
    public string Name { get; init; } = default!;
    public string RackId { get; init; } = default!; // FK ke tabel Racks
    public int Category { get; init; }
    public int TotalStock { get; init; }
    public string Unit { get; init; } = default!;
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
{
    private readonly ISolutionTemplateDbContext _context;

    public CreateItemCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        // 1. VALIDASI: Cek apakah RackId yang diinput Admin ada di tabel Racks
        var rack = await _context.Racks
            .FirstOrDefaultAsync(r => r.RackId == request.RackId, cancellationToken);

        if (rack == null)
        {
            throw new Exception($"Gagal: Rak dengan ID '{request.RackId}' tidak ditemukan di sistem!");
        }

        // 2. OPSIONAL: Cek kalau Rak statusnya Full, kasih peringatan (tergantung kebijakan lu)
        if (rack.Status == RackStatus.Full)
        {
            // Bisa throw error atau sekedar log, tapi mending throw biar Admin sadar
            throw new Exception($"Gagal: Rak '{request.RackId}' saat ini berstatus FULL!");
        }

        var entity = new Item
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            RackId = request.RackId,
            Category = (ItemCategory)request.Category,
            TotalStock = request.TotalStock,
            AvailableStock = request.TotalStock, // Awalnya stok tersedia sama dengan total
            Unit = request.Unit,
            Status = 0, // 0: Pending/Created
            IsDeleted = false
        };

        _context.Items.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}