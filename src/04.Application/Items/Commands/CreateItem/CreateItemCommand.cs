using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;

// 1. Ganti return type IRequest dari int ke Guid
public class CreateItemCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public ItemCategory Category { get; set; }
    public string? RackId { get; set; }
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImageUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

// 2. Ganti IRequestHandler dari int ke Guid
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
{
    private readonly ISolutionTemplateDbContext _context;

    public CreateItemCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Item
        {
            // ID tidak perlu diisi manual, biasanya auto-generate Guid.NewGuid() di constructor Entity
            Name = request.Name,
            Category = request.Category,
            RackId = request.RackId,
            Status = ItemStatus.Pending,
            TotalStock = request.TotalStock,
            AvailableStock = request.AvailableStock,
            Unit = request.Unit,
            ImageUrl = request.ImageUrl,
            ExpiryDate = request.ExpiryDate
        };

        _context.Items.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        // Sekarang return entity.Id yang bertipe Guid
        return entity.Id;
    }
}   