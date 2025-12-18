using Pertamina.SolutionTemplate.Application.Services.Sms.Models.SendSms;

namespace Pertamina.SolutionTemplate.Application.Services.Sms;

public interface ISmsService
{
    Task SendSmsAsync(SendSmsRequest sendSmsRequest);
}
