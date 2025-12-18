using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Application.Data.Commands.CreatedDraftCatalogData;
using Pertamina.SolutionTemplate.Application.Data.Commands.UpdatedDraftCatalogData;
using Pertamina.SolutionTemplate.Application.Data.Queries.GetDatas;
using Pertamina.SolutionTemplate.Application.Data.Queries.GetDatasPaginated;
using Pertamina.SolutionTemplate.Application.Data.Queries.GetRequestData;
using Pertamina.SolutionTemplate.Application.Data.Queries.GetTableMasterData;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Constants;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetAllMasterData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[ApiVersion(ApiVersioning.V1.Number)]
public class DataController : ApiControllerBase
{
    [Authorize]
    [HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.AllData)]
    [Produces(typeof(ListResponse<GetSingleData>))]
    public async Task<ActionResult<ListResponse<GetSingleData>>> GetAllData()
    {
        return await Mediator.Send(new GetDatasQuery());
    }
    [HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.GetAllDataPaginated)]
    [Produces(typeof(PaginatedListResponse<GetSingleData>))]
    public async Task<ActionResult<PaginatedListResponse<GetSingleData>>> GetAllDataPaginated([FromQuery] GetDatasPaginatedQuery query)
    {
        return await Mediator.Send(query);
    }
    [Authorize]
    [HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.GetTableMasterData)]
    [Produces(typeof(ListResponse<GetAllMasterData>))]
    public async Task<ActionResult<ListResponse<GetAllMasterData>>> GetTableMasterData()
    {
        return await Mediator.Send(new GetTableMasterDataQuery());
    }
    [Authorize]
    [HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.GetTableRequestData)]
    [Produces(typeof(ListResponse<GetSingleRequestData>))]
    public async Task<ActionResult<ListResponse<GetSingleRequestData>>> GetTableRequestData()
    {
        return await Mediator.Send(new GetRequestDataQuery());
    }
    //[Authorize]
    //[HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.GetTableHistoricalApplicationPhase)]
    //[Produces(typeof(ListResponse<GetSingleDataHistoricalApplicationPhase>))]
    //public async Task<ActionResult<ListResponse<GetSingleDataHistoricalApplicationPhase>>> GetTableHistoricalApplicationPhase()
    //{
    //    return await Mediator.Send(new GetHistoricalApplicationPhaseQuery());
    //}
    //[Authorize]
    //[HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.GetTableDraftHistoricalApplicationPhase)]
    //[Produces(typeof(ListResponse<GetSingleDataHistoricalApplicationPhase>))]
    //public async Task<ActionResult<ListResponse<GetSingleDataDraftHistoricalApplicationPhase>>> GetTableDraftHistoricalApplicationPhase()
    //{
    //    return await Mediator.Send(new GetDraftHistoricalApplicationPhaseQuery());
    //}
    [Authorize]
    [HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.AddDraftCatalog)]
    [Consumes(ContentTypes.ApplicationXWwwFormUrlEncoded)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AddDraftCatalogResponse>> AddDraftCatalog([FromForm] CreatedDraftCatalogDataCommands command)
    {
        return CreatedAtAction(nameof(AddDraftCatalog), await Mediator.Send(command));
    }
    [Authorize]
    [HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.UpdateDraftCatalog)]
    [Consumes(ContentTypes.ApplicationXWwwFormUrlEncoded)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AddDraftCatalogResponse>> UpdateDraftCatalog([FromForm] UpdatedDraftCatalogDataCommands command)
    {
        return CreatedAtAction(nameof(UpdateDraftCatalog), await Mediator.Send(command));
    }
    //[Authorize]
    //[HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.AddDraftHistoricalApplicationPhase)]
    //[Consumes(ContentTypes.ApplicationXWwwFormUrlEncoded)]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //public async Task<ActionResult<AddDraftHistoricalApplicationPhaseResponse>> AddDraftHistoricalApplicationPhase([FromForm] CreatedDraftHistoricalApplicationPhaseCommands command)
    //{
    //    return CreatedAtAction(nameof(AddDraftHistoricalApplicationPhase), await Mediator.Send(command));
    //}
    //[Authorize]
    //[HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.UpdateDraftHistoricalApplicationPhase)]
    //[Consumes(ContentTypes.ApplicationXWwwFormUrlEncoded)]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //public async Task<ActionResult<AddDraftHistoricalApplicationPhaseResponse>> UpdateDraftHistoricalApplicationPhase([FromForm] UpdatedDraftHistoricalApplicationPhaseCommands command)
    //{
    //    return CreatedAtAction(nameof(UpdateDraftHistoricalApplicationPhase), await Mediator.Send(command));
    //}
    //[Authorize]
    //[HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.SingleDataByKey)]
    //[Produces(typeof(ListResponse<GetSingleData>))]
    //public async Task<ActionResult<ListResponse<GetSingleData>>> GetSingleDataByKey()
    //{
    //    return await Mediator.Send(new GetDatasQuery());
    //}

    //[Authorize]
    //[HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.InsertData)]
    //[Produces(typeof(string))]
    //public async Task<ActionResult<ListResponse<GetSingleData>>> InsertData([FromRoute] GetAllDataByToken? dataForm)
    //{
    //    return await Mediator.Send(new GetDatasByNameQuery { AppValue = dataForm.AppName.ToString() });
    //}

    //[Authorize]
    //[HttpGet(ApiEndpoint.V1.Data.RouteTemplateFor.UpdateData)]
    //[Produces(typeof(ListResponse<GetSingleData>))]
    //public async Task<ActionResult<ListResponse<GetSingleData>>> UpdateData([FromRoute] GetAllDataByToken? dataForm)
    //{
    //    return await Mediator.Send(new GetDatasByNameQuery { AppValue = dataForm.AppName.ToString() });
    //}

    //[Authorize]
    //[HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.DeleteData)]
    //[Produces(typeof(ListResponse<GetSingleData>))]
    //public async Task<ActionResult<ListResponse<GetSingleData>>> DeleteDataByKey([FromRoute] GetSingleDataByKeyAndToken? dataForm)
    //{
    //    if (dataForm.Key == "id")
    //    {
    //        return await Mediator.Send(new GetDatasByNameQuery { AppValue = dataForm.KeyValue.ToString() });
    //    }
    //    else
    //    {
    //        return await Mediator.Send(new GetDatasByNameQuery { AppValue = dataForm.KeyValue.ToString() });
    //    }
    //}
}

