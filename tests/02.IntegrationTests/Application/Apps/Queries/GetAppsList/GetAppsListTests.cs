//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Queries.GetAppsList;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Queries.GetAppsList;

//[Collection(nameof(ApplicationFixture))]
//public class GetAppsListTests
//{
//    private readonly ApplicationFixture _fixture;

//    public GetAppsListTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Get_Apps_List()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var appsCount = 5;

//        for (var i = 1; i <= appsCount; i++)
//        {
//            var createAppCommand = new CreateAppCommand
//            {
//                Name = $"App {i}",
//                Description = $"App {i} Description"
//            };

//            await _fixture.SendAsync(createAppCommand);
//        }

//        _fixture.RunAsUnauthenticatedUser();

//        var query = new GetAppsListQuery();
//        var result = await _fixture.SendAsync(query);

//        result.Items.Count.Should().Be(appsCount);
//    }
//}
