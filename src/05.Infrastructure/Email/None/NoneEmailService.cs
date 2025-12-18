using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Application.Services.Email;
using Pertamina.SolutionTemplate.Application.Services.Email.Models.SendEmail;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Email.None;

public class NoneEmailService : IEmailService
{
    private readonly ILogger<NoneEmailService> _logger;

    public NoneEmailService(ILogger<NoneEmailService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(Email)} {CommonDisplayTextFor.Service}");
    }

    public Task SendEmailAsync(SendEmailRequest emailModel)
    {
        LogWarning();

        return Task.CompletedTask;
    }
}
