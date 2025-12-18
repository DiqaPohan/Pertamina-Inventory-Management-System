using Pertamina.SolutionTemplate.Application.Services.Otp.Models.CreateOtp;

namespace Pertamina.SolutionTemplate.Application.Services.Otp;

public interface IOtpService
{
    CreateOtpResponse CreateOtp(string username);
    string GetCode(string secret, bool isTotp = true);
    byte[] GetGraphic(string otpUrl);
    bool VerifyCode(string secret, string code);
}
