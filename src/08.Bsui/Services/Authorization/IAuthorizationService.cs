using Pertamina.SolutionTemplate.Shared.Services.Authorization.Models.GetAuthorizationInfo;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Models.GetPositions;

namespace Pertamina.SolutionTemplate.Bsui.Services.Authorization;

public interface IAuthorizationService
{
    Task<GetPositionsResponse> GetPositionsAsync(string username, string accessToken, CancellationToken cancellationToken = default);
    Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, string accessToken, CancellationToken cancellationToken = default);
}
