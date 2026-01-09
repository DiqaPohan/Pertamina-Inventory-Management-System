using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.Application.Loans.Queries.GetLoans;

public class GetLoansQuery : PaginatedListRequest, IRequest<PaginatedListResponse<LoanTransaction>>
{
}

public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, PaginatedListResponse<LoanTransaction>>
{
    private readonly ISolutionTemplateDbContext _context;

    public GetLoansQueryHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedListResponse<LoanTransaction>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        // Query dasar ke tabel LoanTransactions
        var query = _context.LoanTransactions
            .Include(x => x.Item)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        // Ambil data peminjaman (loans), bukan items
        var loans = await query
            .OrderByDescending(x => x.Created)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Kembalikan dalam format PaginatedListResponse
        return new PaginatedListResponse<LoanTransaction>
        {
            Items = loans, // Properti di DTO Response biasanya tetap bernama 'Items' secara umum, tapi datanya berisi list 'loans'
            TotalCount = totalCount
        };
    }
}