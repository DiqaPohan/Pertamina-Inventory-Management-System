using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.ConfirmPlacement;

public record ConfirmPlacementCommand(Guid Id) : IRequest<bool>;

public class ConfirmPlacementCommandHandler : IRequestHandler<ConfirmPlacementCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public ConfirmPlacementCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ConfirmPlacementCommand request, CancellationToken cancellationToken)
    {
        // Cari barang berdasarkan ID yang dikirim dari scan QR / dropdown
        var entity = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException("Barang tidak ditemukan!");

        // Ubah status jadi Active karena pegawai sudah konfirmasi naruh di rak
        entity.Status = ItemStatus.Active;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}