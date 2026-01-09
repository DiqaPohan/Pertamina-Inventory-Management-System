using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Common.Requests;  // Penting
using Pertamina.SolutionTemplate.Shared.Common.Responses; // Penting
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItems
{
    // 1. Ubah warisan menjadi PaginatedListRequest agar bisa menangkap ?PageNumber=1&PageSize=100
    // 2. Ubah Return type menjadi PaginatedListResponse<Item>
    public class GetItemsQuery : PaginatedListRequest, IRequest<PaginatedListResponse<Item>>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, PaginatedListResponse<Item>>
    {
        private readonly ISolutionTemplateDbContext _context;

        public GetItemsQueryHandler(ISolutionTemplateDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedListResponse<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            // Query dasar
            var query = _context.Items.AsNoTracking();

            // Hitung total data sebelum dipotong halaman
            var totalCount = await query.CountAsync(cancellationToken);

            // Ambil data sesuai halaman (Skip & Take)
            var items = await query
                .OrderBy(x => x.Id) // Wajib ada OrderBy saat paging
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Kembalikan dalam format PaginatedListResponse yang diinginkan Frontend
            return new PaginatedListResponse<Item>
            {
                Items = items,
                TotalCount = totalCount
                // If PaginatedListResponse has properties for Page and PageSize, set them here as well.
            };
        }
    }
}