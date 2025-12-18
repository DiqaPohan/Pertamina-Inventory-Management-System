//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.DeleteApp;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Commands.DeleteApp;

//[Collection(nameof(ApplicationFixture))]
//public class DeleteAppTests
//{
//    private readonly ApplicationFixture _fixture;

//    public DeleteAppTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Delete_App()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var createAppCommand = new CreateAppCommand
//        {
//            Name = "App 1 Name",
//            Description = "App 1 Description"
//        };

//        var response = await _fixture.SendAsync(createAppCommand);

//        var app = await _fixture.FindAsync<App>(response.AppId);

//        var deleteAppCommand = new DeleteAppCommand
//        {
//            AppId = app!.Id
//        };

//        await _fixture.SendAsync(deleteAppCommand);

//        app = await _fixture.FindAsync<App>(response.AppId);

//        app!.IsDeleted.Should().Be(true);
//    }
//}
