using MediatR;
using Domain.Entities;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;

public class UpdateItemCommand : IRequest<bool>
{
    public Guid Id { get; set; } // Wajib ada untuk identitas
    public string Name { get; set; } = string.Empty;
    public string? RackId { get; set; }
    public ItemStatus Status { get; set; }
    public ItemCategory Category { get; set; }
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImageUrl { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public UpdateItemCommandHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        entity.Name = request.Name;
        entity.Category = request.Category;
        entity.RackId = request.RackId;
        entity.TotalStock = request.TotalStock;
        entity.AvailableStock = request.AvailableStock;
        entity.Unit = request.Unit;
        entity.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}