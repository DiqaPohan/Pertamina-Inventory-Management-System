using Pertamina.SolutionTemplate.Application.Services.DateAndTime;

namespace Pertamina.SolutionTemplate.Infrastructure.DateAndTime;

public class DateAndTimeService : IDateAndTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
