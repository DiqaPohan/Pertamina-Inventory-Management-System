using Application.Services.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Shared.Common.Enums; // Pastikan namespace Enum ini benar
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Application.Items.Queries.GetItems
{
    public class GetItemsQuery : PaginatedListRequest, IRequest<PaginatedListResponse<Item>>
    {
        // Parameter tambahan untuk filter pegawai (Scan QR) dan Dashboard Admin
        public string? RackId { get; set; }
        public ItemStatus? Status { get; set; }
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
            // 1. Query dasar
            var query = _context.Items.AsNoTracking();

            // 2. Filter berdasarkan RackId (Penting untuk scan QR Pegawai)
            if (!string.IsNullOrEmpty(request.RackId))
            {
                query = query.Where(x => x.RackId == request.RackId);
            }

            // 3. Filter berdasarkan Status (Pending/Active)
            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status);
            }

            // 4. Hitung total data SETELAH filter, tapi SEBELUM paging
            var totalCount = await query.CountAsync(cancellationToken);

            // 5. Ambil data dengan Paging
            var items = await query
                .OrderBy(x => x.Name) // Urutkan berdasarkan nama agar rapi di tabel
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // 6. Return response
            return new PaginatedListResponse<Item>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}