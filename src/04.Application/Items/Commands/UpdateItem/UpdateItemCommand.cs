using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;

public class UpdateItemCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RackId { get; set; } = string.Empty; // Sekarang wajib valid karena FK
    public ItemStatus Status { get; set; }
    public ItemCategory Category { get; set; }
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImageUrl { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public UpdateItemCommandHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        // 1. Cari barang yang mau diupdate
        var entity = await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null) return false;

        // 2. VALIDASI RACK: Jika RackId diubah, cek apakah rak baru ada di database?
        if (entity.RackId != request.RackId)
        {
            var rackExists = await _context.Racks.AnyAsync(r => r.RackId == request.RackId, cancellationToken);

            if (!rackExists)
            {
                // Kalau rak nggak terdaftar, kita kasih tau Admin
                throw new Exception($"Gagal Update: Rak dengan ID '{request.RackId}' tidak terdaftar di sistem!");
            }

            // Opsional: Bisa cek juga apakah rak tujuan penuh (RackStatus.Full)
            // Tapi biasanya Admin punya wewenang buat tetep naro barang disitu kalau darurat
        }

        // 3. Mapping perubahan
        entity.Name = request.Name;
        entity.Category = request.Category;
        entity.RackId = request.RackId;
        entity.TotalStock = request.TotalStock;
        entity.AvailableStock = request.AvailableStock;
        entity.Unit = request.Unit;
        entity.ImageUrl = request.ImageUrl;
        entity.Status = request.Status; // Tambahkan status juga kalau Admin mau ganti manual

        // 4. Simpan ke SQL Server
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}