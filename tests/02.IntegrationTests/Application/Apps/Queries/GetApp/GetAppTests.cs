//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Queries.GetApp;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Queries.GetApp;

//[Collection(nameof(ApplicationFixture))]
//public class GetAppTests
//{
//    private readonly ApplicationFixture _fixture;

//    public GetAppTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Get_App()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var createAppCommand = new CreateAppCommand
//        {
//            Name = "App 01",
//            Description = "App 01 Description"
//        };

//        var response = await _fixture.SendAsync(createAppCommand);

//        var query = new GetAppQuery
//        {
//            AppId = response.AppId
//        };

//        var getAppResponse = await _fixture.SendAsync(query);

//        getAppResponse.Should().NotBeNull();
//        getAppResponse.Name.Should().Be(createAppCommand.Name);
//        getAppResponse.Description.Should().Be(createAppCommand.Description);
//    }
//}
