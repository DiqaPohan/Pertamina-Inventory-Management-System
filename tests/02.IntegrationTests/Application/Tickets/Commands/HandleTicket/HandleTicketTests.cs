//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Apps.Commands.CreateApp;
//using Pertamina.SolutionTemplate.Application.Tickets.Commands.CreateTicket;
//using Pertamina.SolutionTemplate.Application.Tickets.Commands.HandleTicket;
//using Pertamina.SolutionTemplate.Application.Tickets.Commands.SubmitTicket;
//using Pertamina.SolutionTemplate.Application.Users.Commands.CreateMyProfile;
//using Pertamina.SolutionTemplate.Application.Users.Commands.VerifyMyProfile;
//using Pertamina.SolutionTemplate.Application.Users.Queries.GetMyVerificationCode;
//using Pertamina.SolutionTemplate.Base.Tickets.Enums;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Tickets.Commands.HandleTicket;

//[Collection(nameof(ApplicationFixture))]
//public class HandleTicketTests
//{
//    private readonly ApplicationFixture _fixture;

//    public HandleTicketTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Handle_Ticket()
//    {
//        #region Create App
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.AstTeknologiInformasi);

//        var createAppCommand = new CreateAppCommand
//        {
//            Name = "App 1 Name",
//            Description = "App 1 Description"
//        };

//        var createAppResponse = await _fixture.SendAsync(createAppCommand);

//        var app = await _fixture.FindAsync<App>(createAppResponse.AppId);

//        app.Should().NotBeNull();
//        #endregion Create App

//        #region Create Profile
//        _fixture.RunAsUser(UsernameFor.TicketingUser1);

//        var createMyProfileCommand = new CreateMyProfileCommand();

//        var createMyProfileResponse = await _fixture.SendAsync(createMyProfileCommand);

//        var userTicketingUser1 = await _fixture.FindAsync<User>(createMyProfileResponse.UserId);

//        userTicketingUser1.Should().NotBeNull();
//        userTicketingUser1!.Id.Should().Be(_fixture.CurrentUser.UserId!.Value);
//        userTicketingUser1.Username.Should().Be(_fixture.CurrentUser.Username);
//        #endregion Create Profile

//        #region Verify Profile
//        _fixture.RunAsUser(UsernameFor.TicketingUser1);

//        var getMyVerificationCodeQuery = new GetMyVerificationCodeQuery();

//        var getMyVerificationCodeResponse = await _fixture.SendAsync(getMyVerificationCodeQuery);

//        var verifyMyProfileCommand = new VerifyMyProfileCommand
//        {
//            VerificationCode = getMyVerificationCodeResponse.VerificationCode
//        };

//        await _fixture.SendAsync(verifyMyProfileCommand);

//        userTicketingUser1 = await _fixture.FindAsync<User>(createMyProfileResponse.UserId);

//        userTicketingUser1!.IsVerified.Should().Be(true);
//        #endregion Verify User

//        #region Create Ticket
//        _fixture.RunAsUser(UsernameFor.TicketingUser1);

//        var createTicketCommand = new CreateTicketCommand
//        {
//            AppId = app!.Id,
//            Title = "Ticket 1 Title",
//            SeverityLevel = SeverityLevel.High,
//            Description = "Ticket 1 Description"
//        };

//        var createTicketResponse = await _fixture.SendAsync(createTicketCommand);

//        var ticket = await _fixture.FindAsync<Ticket>(createTicketResponse.TicketId);

//        ticket.Should().NotBeNull();
//        ticket!.Title.Should().Be(createTicketCommand.Title);
//        ticket.Description.Should().Be(createTicketCommand.Description);
//        ticket.Status.Should().Be(TicketStatus.Draft);
//        #endregion Create Ticket

//        #region Submit Ticket
//        _fixture.RunAsUser(UsernameFor.TicketingUser1);

//        var submitTicketCommand = new SubmitTicketCommand
//        {
//            TicketId = ticket.Id
//        };

//        await _fixture.SendAsync(submitTicketCommand);

//        ticket = await _fixture.FindAsync<Ticket>(ticket.Id);

//        ticket!.Status.Should().Be(TicketStatus.Submitted);
//        #endregion Submit Ticket

//        #region Handle Ticket
//        _fixture.RunAsUser(UsernameFor.TicketingSupportEngineer1, PositionIdFor.JrAnalystApplicationSupport1);

//        var handleTicketCommand = new HandleTicketCommand
//        {
//            TicketId = ticket.Id
//        };

//        await _fixture.SendAsync(handleTicketCommand);

//        ticket = await _fixture.FindAsync<Ticket>(ticket.Id);

//        ticket!.Status.Should().Be(TicketStatus.InProgress);
//        #endregion Handle Ticket
//    }
//}
