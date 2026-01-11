using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<Guid>
{
    public string Name { get; init; } = default!;
    public string RackId { get; init; } = default!;
    public int Category { get; init; }
    public int TotalStock { get; init; }
    public string Unit { get; init; } = default!;
    public string? ImageUrl { get; init; }
    public DateTime? ExpiryDate { get; init; }
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
        // 1. Bersihkan inputan biar gak ada spasi gaib yang bikin pencarian gagal
        var cleanName = request.Name.Trim();
        var cleanRackId = request.RackId.Trim();

        // 2. Cek apakah Rak tersedia
        var rack = await _context.Racks
            .FirstOrDefaultAsync(r => r.RackId == cleanRackId, cancellationToken);

        if (rack == null)
            throw new Exception($"Gagal: Lokasi Rak '{cleanRackId}' tidak ditemukan di database!");

        // 3. LOGIC ANTI-DUPLIKASI (Pencarian barang yang sudah ada di rak tersebut)
        var existingItem = await _context.Items
            .Where(x => x.Name.ToLower() == cleanName.ToLower()
                     && x.RackId == cleanRackId
                     && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingItem != null)
        {
            // --- JIKA BARANG SUDAH ADA, UPDATE STOK SAJA ---
            existingItem.TotalStock += request.TotalStock;
            existingItem.AvailableStock += request.TotalStock;

            // Update info opsional kalau lu mau (misal update gambar terbaru)
            if (!string.IsNullOrEmpty(request.ImageUrl))
            {
                existingItem.ImageUrl = request.ImageUrl;
            }

            existingItem.Modified = DateTimeOffset.Now;
            existingItem.ModifiedBy = "Admin-Upsert";

            await _context.SaveChangesAsync(cancellationToken);

            // Kembalikan ID barang yang lama (biar FE gak bingung)
            return existingItem.Id;
        }

        // 4. JIKA BARANG BELUM ADA, BARU BUAT BARU
        var entity = new Item
        {
            Id = Guid.NewGuid(),
            Name = cleanName,
            RackId = cleanRackId,
            Category = (ItemCategory)request.Category,
            TotalStock = request.TotalStock,
            AvailableStock = request.TotalStock,
            Unit = request.Unit,
            ImageUrl = request.ImageUrl,
            ExpiryDate = request.ExpiryDate,
            Status = ItemStatus.Pending,
            IsDeleted = false,
            Created = DateTimeOffset.Now,
            CreatedBy = "Admin-Manual"
        };

        _context.Items.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}