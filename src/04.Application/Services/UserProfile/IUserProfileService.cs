using Pertamina.SolutionTemplate.Application.Services.UserProfile.Models.GetUserProfile;

namespace Pertamina.SolutionTemplate.Application.Services.UserProfile;

public interface IUserProfileService
{
    Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken);
}
