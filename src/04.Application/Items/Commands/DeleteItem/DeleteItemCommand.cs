using MediatR;
using Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Pertamina.SolutionTemplate.Application.Items.Commands.DeleteItem;

public record DeleteItemCommand(Guid Id) : IRequest<bool>;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
{
    private readonly ISolutionTemplateDbContext _context;

    public DeleteItemCommandHandler(ISolutionTemplateDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null) return false;

        _context.Items.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}