using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums; // Pastikan namespace enum bener
using Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Loans.Commands.Create;

public record CreateLoanCommand : IRequest<Guid>
{
    public Guid ItemId { get; init; }
    public string BorrowerName { get; init; } = string.Empty;
    public string BorrowerPhone { get; init; } = string.Empty;
    public DateTime LoanDate { get; init; }
    public DateTime DueDate { get; init; }
}

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
{
    private readonly ISolutionTemplateDbContext _context;

    public CreateLoanCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        // 1. VALIDASI BARANG: Cek barangnya ada, statusnya ACTIVE, dan stoknya cukup
        var item = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (item == null)
            throw new Exception("Barang tidak ditemukan!");

        // CEK STATUS: Barang yang bisa dipinjam cuma yang statusnya Active (sudah di rak)
        if (item.Status != ItemStatus.Active)
            throw new Exception($"Barang '{item.Name}' belum tersedia untuk dipinjam (Status: {item.Status}).");

        // CEK STOK: Jangan sampe stok minus
        if (item.AvailableStock <= 0)
            throw new Exception($"Stok barang '{item.Name}' sudah habis!");

        // 2. LOGIC PENGURANGAN STOK: Kurangi stok tersedia saat dipinjam
        item.AvailableStock -= 1;

        // 3. CREATE TRANSAKSI
        var entity = new LoanTransaction
        {
            Id = Guid.NewGuid(), // Tambahin Id Guid
            ItemId = request.ItemId,
            BorrowerName = request.BorrowerName,
            BorrowerPhone = request.BorrowerPhone,
            LoanDate = request.LoanDate,
            DueDate = request.DueDate,
            Status = LoanStatus.Requested,
            QrCodeToken = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
            Created = DateTimeOffset.Now, // Jika pake AuditableEntity
            CreatedBy = "System"
        };

        _context.LoanTransactions.Add(entity);

        // 4. SIMPAN PERUBAHAN: Ini bakal update tabel Items dan Loans sekaligus
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}