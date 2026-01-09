using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.LoanTransactions.Queries.GetLoanById;

public record GetLoanByIdQuery(Guid Id) : IRequest<LoanTransaction?>;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanTransaction?>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetLoanByIdQueryHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<LoanTransaction?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.LoanTransactions
            .Include(x => x.Item) // Menarik data barang terkait
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}