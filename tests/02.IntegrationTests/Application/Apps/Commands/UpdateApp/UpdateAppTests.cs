//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.UpdateApp;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Commands.UpdateApp;

//[Collection(nameof(ApplicationFixture))]
//public class UpdateAppTests
//{
//    private readonly ApplicationFixture _fixture;

//    public UpdateAppTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Update_App()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var createAppCommand = new CreateAppCommand
//        {
//            Name = "App 1 Name",
//            Description = "App 1 Description"
//        };

//        var response = await _fixture.SendAsync(createAppCommand);

//        var app = await _fixture.FindAsync<App>(response.AppId);

//        app!.Name.Should().Be(createAppCommand.Name);
//        app.Description.Should().Be(createAppCommand.Description);

//        var updateAppCommand = new UpdateAppCommand
//        {
//            AppId = app.Id,
//            Name = "App 1 Name Updated",
//            Description = "App 1 Description Updated"
//        };

//        await _fixture.SendAsync(updateAppCommand);

//        app = await _fixture.FindAsync<App>(response.AppId);

//        app!.Name.Should().Be(updateAppCommand.Name);
//        app.Description.Should().Be(updateAppCommand.Description);
//    }
//}
