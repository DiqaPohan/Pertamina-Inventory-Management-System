using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.Application.Loans.Queries.GetLoans;

public class GetLoansQuery : PaginatedListRequest, IRequest<PaginatedListResponse<LoanTransaction>>
{
    // Lu bisa tambahin properti filter di sini kalau nanti butuh, 
    // misal: public string? Search { get; set; }
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
        // 1. Query dasar dengan Nested Include: Loan -> Item -> Rack
        var query = _context.LoanTransactions
            .Include(x => x.Item)
                .ThenInclude(i => i.Rack)
            .AsNoTracking();

        // 2. Filter data yang tidak dihapus (Standard template)
        query = query.Where(x => !x.IsDeleted);

        // 3. Hitung total data
        var totalCount = await query.CountAsync(cancellationToken);

        // 4. Ambil data dengan Paging
        var loans = await query
            .OrderByDescending(x => x.Created) // Urutkan dari yang terbaru
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // 5. Kembalikan dalam format PaginatedListResponse
        return new PaginatedListResponse<LoanTransaction>
        {
            Items = loans,
            TotalCount = totalCount
        };
    }
}