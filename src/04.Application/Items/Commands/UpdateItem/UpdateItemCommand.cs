using Application.Services.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;

public record UpdateItemCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string RackId { get; init; } = default!;
    public int Category { get; init; }
    public int TotalStock { get; init; }
    public string Unit { get; init; } = default!;
    public string? ImageUrl { get; init; } // TAMBAHAN: Biar bisa ganti foto pas Edit
    public DateTime? ExpiryDate { get; init; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public UpdateItemCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        // 1. Cari data barang berdasarkan ID
        var entity = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        // 2. Update Properti
        entity.Name = request.Name;
        entity.RackId = request.RackId;
        entity.Category = (ItemCategory)request.Category;
        entity.TotalStock = request.TotalStock;
        entity.Unit = request.Unit;
        entity.ImageUrl = request.ImageUrl; // SEKARANG DI-UPDATE: Foto baru tersimpan
        entity.ExpiryDate = request.ExpiryDate;

        entity.Modified = DateTimeOffset.Now;
        entity.ModifiedBy = "Admin-Manual";

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}