using Pertamina.SolutionTemplate.Application.Services.Email.Models.SendEmail;

namespace Pertamina.SolutionTemplate.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailRequest sendEmailRequest);
}
