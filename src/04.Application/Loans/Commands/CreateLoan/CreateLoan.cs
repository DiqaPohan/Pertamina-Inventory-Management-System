using MediatR;
using Domain.Entities;
using Shared.Common.Enums;
using Application.Services.Persistence;

namespace Pertamina.SolutionTemplate.Application.Loans.Commands.Create;

public record CreateLoanCommand : IRequest<Guid>
{
    public Guid ItemId { get; init; }
    public string BorrowerName { get; init; } = string.Empty;
    public string BorrowerPhone { get; init; } = string.Empty;
    public DateTime LoanDate { get; init; }
    public DateTime DueDate { get; init; }
}

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
{
    private readonly ISolutionTemplateDbContext _context;

    public CreateLoanCommandHandler(ISolutionTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var entity = new LoanTransaction
        {
            ItemId = request.ItemId,
            BorrowerName = request.BorrowerName,
            BorrowerPhone = request.BorrowerPhone,
            LoanDate = request.LoanDate,
            DueDate = request.DueDate,
            Status = LoanStatus.Requested,
            QrCodeToken = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
        };

        _context.LoanTransactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
