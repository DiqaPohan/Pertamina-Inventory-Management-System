using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Common.Exceptions;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.ConfirmPlacement;

// Tambahkan ScannedRackId biar tau barang ditaruh di rak mana pas konfirmasi
public record ConfirmPlacementCommand(Guid Id, string ScannedRackId) : IRequest<bool>;

public class ConfirmPlacementCommandHandler : IRequestHandler<ConfirmPlacementCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public ConfirmPlacementCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ConfirmPlacementCommand request, CancellationToken cancellationToken)
    {
        // 1. Cari barangnya
        var entity = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException("Barang tidak ditemukan!");

        // 2. VALIDASI: Cek apakah Rak yang di-scan pegawai ada di database?
        var rack = await _context.Racks
            .FirstOrDefaultAsync(r => r.RackId == request.ScannedRackId, cancellationToken);

        if (rack == null)
            throw new Exception($"QR Code Rak '{request.ScannedRackId}' tidak terdaftar di sistem!");

        // 3. LOGIC TAMBAHAN (Optional tapi Keren): 
        // Kalau rak yang di-scan beda sama rencana awal (RackId di database), 
        // sistem bakal otomatis update ke lokasi baru hasil scan pegawai.
        entity.RackId = request.ScannedRackId;

        // 4. Ubah status jadi Active (Pakai Enum)
        entity.Status = ItemStatus.Active;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}