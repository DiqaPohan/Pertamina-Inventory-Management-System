//using Microsoft.Extensions.Logging;
//using Pertamina.SolutionTemplate.Application.Common.Exceptions;
//using Pertamina.SolutionTemplate.Application.Services.Authorization;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Positions;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Positions.Models;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Roles;
//using Pertamina.SolutionTemplate.Shared.Services.Authorization.Models.GetAuthorizationInfo;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Infrastructure.Authorization;

//public class FileBasedAuthorizationService : IAuthorizationService
//{
//    private readonly ILogger<FileBasedAuthorizationService> _logger;

//    public FileBasedAuthorizationService(ILogger<FileBasedAuthorizationService> logger)
//    {
//        _logger = logger;
//    }

//    public Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken)
//    {
//        try
//        {
//            var position = PositionRepository.Positions.FirstOrDefault(x => x.Id == positionId);

//            if (position is null)
//            {
//                throw new NotFoundException(nameof(Position), positionId);
//            }

//            var result = new GetAuthorizationInfoResponse();

//            foreach (var roleName in position.RoleNames)
//            {
//                var role = RoleRepository.Roles.FirstOrDefault(x => x.Name == roleName);

//                if (role is not null)
//                {
//                    result.Roles.Add(new GetAuthorizationInfoRole
//                    {
//                        Name = roleName,
//                        Permissions = role.Scopes
//                    });
//                }
//            }

//            foreach (var customParameter in position.CustomParameters)
//            {
//                result.CustomParameters.Add(new GetAuthorizationInfoCustomParameter
//                {
//                    Key = customParameter.Key,
//                    Value = customParameter.Value
//                });
//            }

//            return Task.FromResult(result);
//        }
//        catch (Exception exception)
//        {
//            _logger.LogError(exception, $"Error in executing method {nameof(GetAuthorizationInfoAsync)}");

//            throw;
//        }
//    }
//}
