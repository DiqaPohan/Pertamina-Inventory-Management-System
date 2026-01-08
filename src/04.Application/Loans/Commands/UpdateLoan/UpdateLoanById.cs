using MediatR;
using Shared.Common.Enums;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;

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
        // 1. Cari data berdasarkan ID
        var entity = await _context.LoanTransactions
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // 2. Jika tidak ditemukan, kembalikan false atau lempar Exception
        if (entity == null)
        {
            return false;
        }

        // 3. Update hanya statusnya
        entity.Status = request.Status;

        // Logika tambahan: Jika status berubah jadi Returned (3), isi ReturnDate secara otomatis
        if (request.Status == LoanStatus.Returned)
        {
            entity.ReturnDate = DateTime.Now;
        }

        // 4. Simpan perubahan
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}