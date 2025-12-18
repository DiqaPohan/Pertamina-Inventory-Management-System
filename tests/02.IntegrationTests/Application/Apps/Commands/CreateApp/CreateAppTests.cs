//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Commands.CreateApp;

//[Collection(nameof(ApplicationFixture))]
//public class CreateAppTests
//{
//    private readonly ApplicationFixture _fixture;

//    public CreateAppTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Create_App()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var command = new CreateAppCommand
//        {
//            Name = "App 1 Name",
//            Description = "App 1 Description"
//        };

//        var response = await _fixture.SendAsync(command);

//        var app = await _fixture.FindAsync<App>(response.AppId);

//        app.Should().NotBeNull();
//        app!.Name.Should().Be(command.Name);
//        app.Description.Should().Be(command.Description);
//        app.CreatedBy.Should().Be(UsernameFor.TicketingMultiRole);
//    }
//}
