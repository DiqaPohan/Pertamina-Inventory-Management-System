using System.Text.Json;
using Microsoft.Extensions.Options;
using Pertamina.SolutionTemplate.Client.Common.Extensions;
using Pertamina.SolutionTemplate.Client.Services.UserInfo;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Requests;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetAllMasterData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Model;
using RestSharp;

namespace Pertamina.SolutionTemplate.Client.Services.BackEnd;

public class DataService
{
    private readonly RestClient _restClient;

    public DataService(IOptions<BackEndOptions> backEndServiceOptions, UserInfoService userInfo)
    {
        _restClient = new RestClient($"{backEndServiceOptions.Value.BaseUrl}/{Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.Segment}");
        _restClient.AddUserInfo(userInfo);
    }
    public async Task<ResponseResult<ListResponse<GetSingleData>>> GetSingleDataByKeyAsync()
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.SingleDataByKey), Method.Get);
        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetSingleData>>();
    }
    public async Task<ResponseResult<PaginatedListResponse<GetSingleData>>> GetAllDataPaginatedAsync(PaginatedListRequest request)
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.GetAllDataPaginated), Method.Get);
        restRequest.AddQueryParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<PaginatedListResponse<GetSingleData>>();
    }

    public async Task<ResponseResult<ListResponse<GetSingleData>>> GetAllDataAsync()
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.AllData), Method.Get);
        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetSingleData>>();
    }
    public async Task<ResponseResult<ListResponse<GetSingleRequestData>>> GetAllRequestDataAsync()
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.GetTableRequestData), Method.Get);
        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetSingleRequestData>>();
    }
    //public async Task<ResponseResult<ListResponse<GetSingleDataDraftHistoricalApplicationPhase>>> GetAllDraftHistoricalApplicationPhaseDataAsync()
    //{
    //    var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.GetTableDraftHistoricalApplicationPhase), Method.Get);
    //    var restResponse = await _restClient.ExecuteAsync(restRequest);

    //    return restResponse.ToResponseResult<ListResponse<GetSingleDataDraftHistoricalApplicationPhase>>();
    //}
    //public async Task<ResponseResult<ListResponse<GetSingleDataHistoricalApplicationPhase>>> GetAllHistoricalApplicationPhaseDataAsync()
    //{
    //    var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.GetTableHistoricalApplicationPhase), Method.Get);
    //    var restResponse = await _restClient.ExecuteAsync(restRequest);

    //    return restResponse.ToResponseResult<ListResponse<GetSingleDataHistoricalApplicationPhase>>();
    //}
    public async Task<ResponseResult<ListResponse<GetAllMasterData>>> GetTableMasterDataAsync()
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.GetTableMasterData), Method.Get);
        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetAllMasterData>>();
    }
    public async Task<ResponseResult<AddDraftCatalogResponse>> AddDraftCatalogAsync(AddDraftCatalogRequest request)
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.AddDraftCatalog), Method.Post);
        restRequest.AddParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<AddDraftCatalogResponse>();
    }
    public async Task<ResponseResult<AddDraftCatalogResponse>> UpdateDraftCatalogAsync(AddDraftCatalogRequest request)
    {
        var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.UpdateDraftCatalog), Method.Post);
        restRequest.AddParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<AddDraftCatalogResponse>();
    }
    public async Task<List<IdamanUsers>> GetUserFromIdaman(string baseUrl,
        string sType, string sTypeDialog,
        string sToken)
    {
        var lReturn = new List<IdamanUsers>();

        try
        {
            var restClient = new RestClient(baseUrl);
            var restRequest = new RestRequest("/v1/users?searchText=" + sType, Method.Get);
            restRequest.AddHeader(HttpHeaderName.Authorization, sToken);
            var restResponse = await restClient.ExecuteAsync(restRequest);
            if (restResponse.IsSuccessful)
            {
                var data = JsonSerializer.Deserialize<MasterJsonIdamanUsers>(restResponse.Content!);
                foreach (var item in data.value)
                {
                    if (sTypeDialog == "bisuserkbo")
                    {
                        if (!string.IsNullOrEmpty(item.position.kbo))
                        {
                            lReturn.Add(item);
                        }
                    }
                    else
                    {
                        lReturn.Add(item);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        return lReturn;
    }
    //public async Task<ResponseResult<AddDraftHistoricalApplicationPhaseResponse>> AddDraftHistoricalApplicationPhaseAsync(AddDraftHistoricalApplicationPhaseRequest request)
    //{
    //    var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.AddDraftHistoricalApplicationPhase), Method.Post);
    //    restRequest.AddParameters(request);

    //    var restResponse = await _restClient.ExecuteAsync(restRequest);

    //    return restResponse.ToResponseResult<AddDraftHistoricalApplicationPhaseResponse>();
    //}
    //public async Task<ResponseResult<AddDraftHistoricalApplicationPhaseResponse>> UpdateDraftHistoricalApplicationPhaseAsync(AddDraftHistoricalApplicationPhaseRequest request)
    //{
    //    var restRequest = new RestRequest(nameof(Pertamina.SolutionTemplate.Shared.Data.Constants.ApiEndpoint.V1.Data.RouteTemplateFor.UpdateDraftHistoricalApplicationPhase), Method.Post);
    //    restRequest.AddParameters(request);

    //    var restResponse = await _restClient.ExecuteAsync(restRequest);

    //    return restResponse.ToResponseResult<AddDraftHistoricalApplicationPhaseResponse>();
    //}
}
