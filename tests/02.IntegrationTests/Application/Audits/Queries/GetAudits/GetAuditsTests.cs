//using FluentAssertions;
//using Pertamina.SolutionTemplate.Application.Audits.Queries.GetAudits;
//using Pertamina.SolutionTemplate.IntegrationTests.Repositories.Constants;
//using Pertamina.SolutionTemplate.Shared.Audits.Queries.GetAudits;
//using Pertamina.SolutionTemplate.Shared.Common.Enums;
//using Xunit;

//namespace Pertamina.SolutionTemplate.IntegrationTests.Application.Audits.Queries.GetAudits;

//[Collection(nameof(ApplicationFixture))]
//public class GetAuditsTests
//{
//    private readonly ApplicationFixture _fixture;

//    public GetAuditsTests(ApplicationFixture fixture)
//    {
//        _fixture = fixture;
//        _fixture.ResetState().Wait();
//    }

//    [Fact]
//    public async Task Should_Get_Audits()
//    {
//        _fixture.RunAsUser(UsernameFor.TicketingMultiRole, PositionIdFor.KepalaTeknologiInformasi);

//        var query = new GetAuditsQuery
//        {
//            Page = 1,
//            PageSize = 10,
//            SearchText = null,
//            SortField = nameof(GetAuditsAudit.Created),
//            SortOrder = SortOrder.Desc
//        };

//        var result = await _fixture.SendAsync(query);

//        result.Items.Count.Should().BeGreaterOrEqualTo(0);
//    }
//}
