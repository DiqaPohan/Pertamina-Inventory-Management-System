//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Queries.GetApps;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Pertamina.SolutionTemplate.Shared.Apps.Queries.GetApps;
//using Pertamina.SolutionTemplate.Shared.Common.Enums;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Queries.GetApps;

//[Collection(nameof(ApplicationFixture))]
//public class GetAppsTests
//{
//    private readonly ApplicationFixture _fixture;

//    public GetAppsTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Get_Apps()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var appsCount = 50;

//        for (var i = 1; i <= appsCount; i++)
//        {
//            var createAppCommand = new CreateAppCommand
//            {
//                Name = $"App {i}",
//                Description = $"App {i} Description"
//            };

//            await _fixture.SendAsync(createAppCommand);
//        }

//        var query = new GetAppsQuery
//        {
//            Page = 1,
//            PageSize = 10,
//            SearchText = null,
//            SortField = nameof(GetAppsApp.Name),
//            SortOrder = SortOrder.Asc
//        };

//        var result = await _fixture.SendAsync(query);

//        result.Items.Count.Should().Be(query.PageSize);
//    }
//}
