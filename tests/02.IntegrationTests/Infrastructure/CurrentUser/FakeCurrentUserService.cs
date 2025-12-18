//using Pertamina.SolutionTemplate.Application.Services.CurrentUser;
//using Pertamina.SolutionTemplate.Base.ValueObjects;
//using Pertamina.SolutionTemplate.Shared.Common.Constants;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Infrastructure.CurrentUser;

//public class FakeCurrentUserService : ICurrentUserService
//{
//    public Guid? UserId { get; }
//    public string Username { get; }
//    public string ClientId => DefaultTextFor.SystemBackgroundJob;
//    public string? PositionId { get; }
//    public string IpAddress => DefaultTextFor.SystemBackgroundJob;
//    public Geolocation? Geolocation { get; }

//    public FakeCurrentUserService()
//    {
//        Username = DefaultTextFor.SystemBackgroundJob;
//    }

//    public FakeCurrentUserService(Guid userId, string username, string? positionId)
//    {
//        UserId = userId;
//        Username = username;
//        PositionId = positionId;
//    }
//}
