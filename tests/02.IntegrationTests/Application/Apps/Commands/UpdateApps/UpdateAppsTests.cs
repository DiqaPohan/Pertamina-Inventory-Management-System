//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.UpdateApps;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Pertamina.SolutionTemplate.Shared.Apps.Commands.UpdateApps;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Apps.Commands.UpdateApps;

//[Collection(nameof(ApplicationFixture))]
//public class UpdateAppsTests
//{
//    private readonly ApplicationFixture _fixture;

//    public UpdateAppsTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Update_Apps()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var appsCount = 5;

//        var apps = new List<UpdateAppsApp>();

//        for (var i = 1; i <= appsCount; i++)
//        {
//            var createAppCommand = new CreateAppCommand
//            {
//                Name = $"App {i} Name",
//                Description = $"App {i} Description"
//            };

//            var createAppResponse = await _fixture.SendAsync(createAppCommand);

//            apps.Add(new UpdateAppsApp
//            {
//                AppId = createAppResponse.AppId,
//                Name = $"App {i} Updated",
//                Description = $"App {i} Description Updated"
//            });
//        }

//        foreach (var notUpdatedApp in apps)
//        {
//            var app = await _fixture.FindAsync<App>(notUpdatedApp.AppId);

//            app!.Name.Should().EndWith("Name");
//            app.Description.Should().EndWith("Description");
//        }

//        var updateAppsCommand = new UpdateAppsCommand
//        {
//            Apps = apps
//        };

//        await _fixture.SendAsync(updateAppsCommand);

//        foreach (var updatedApp in apps)
//        {
//            var app = await _fixture.FindAsync<App>(updatedApp.AppId);

//            app!.Name.Should().EndWith("Updated");
//            app.Description.Should().EndWith("Updated");
//        }
//    }
//}
