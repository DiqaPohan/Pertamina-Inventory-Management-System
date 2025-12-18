namespace Pertamina.SolutionTemplate.Application.Services.DomainEvent;

public interface IDomainEventService
{
    Task Publish(Domain.Events.DomainEvent domainEvent);
}
