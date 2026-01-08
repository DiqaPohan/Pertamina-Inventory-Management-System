using MediatR;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Services.Persistence;

namespace Pertamina.SolutionTemplate.Application.Loans.Queries.GetLoanById;

public record GetLoanByIdQuery(Guid Id) : IRequest<LoanTransaction?>;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanTransaction?>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetLoanByIdQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<LoanTransaction?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.LoanTransactions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}