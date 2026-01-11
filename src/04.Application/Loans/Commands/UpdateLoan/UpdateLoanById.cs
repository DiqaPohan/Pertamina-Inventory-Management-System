using MediatR;
using Shared.Common.Enums;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Loans.Commands.Update;

public record UpdateLoanStatusCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public LoanStatus Status { get; init; }
}

public class UpdateLoanStatusCommandHandler : IRequestHandler<UpdateLoanStatusCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public UpdateLoanStatusCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
    {
        // 1. Cari data transaksi peminjaman, Include Item-nya buat update stok nanti
        var entity = await _context.LoanTransactions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        // Cek jika status sebelumnya sudah Returned, jangan diproses lagi biar stok nggak double nambah
        if (entity.Status == LoanStatus.Returned && request.Status == LoanStatus.Returned)
        {
            return true;
        }

        // 2. Update status transaksi
        entity.Status = request.Status;

        // 3. LOGIKA PENGEMBALIAN BARANG KE RAK
        if (request.Status == LoanStatus.Returned)
        {
            entity.ReturnDate = DateTime.Now;

            // Pastikan barangnya ada, lalu tambahkan stoknya kembali
            if (entity.Item != null)
            {
                entity.Item.AvailableStock += 1;

                // Opsional: Jika stok kembali penuh, pastikan tidak melebihi TotalStock
                if (entity.Item.AvailableStock > entity.Item.TotalStock)
                {
                    entity.Item.AvailableStock = entity.Item.TotalStock;
                }
            }
        }

        // 4. Simpan perubahan (Update tabel Loans & Items sekaligus)
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}