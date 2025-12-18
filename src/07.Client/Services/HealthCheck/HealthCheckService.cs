using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Services.HealthCheck.Constants;
using Pertamina.SolutionTemplate.Shared.Services.HealthCheck.Queries.GetHealthCheck;
using RestSharp;

namespace Pertamina.SolutionTemplate.Client.Services.HealthCheck;

public class HealthCheckService
{
    private readonly RestClient _restClient;

    public HealthCheckService()
    {
        _restClient = new RestClient();
    }

    public async Task<GetHealthCheckResponse> GetHealthCheckAsync(string healthCheckUrl)
    {
        var restRequest = new RestRequest(healthCheckUrl, Method.Get);
        var restResponse = await _restClient.ExecuteAsync<GetHealthCheckResponse>(restRequest);

        if (!restResponse.IsSuccessful)
        {
            return new GetHealthCheckResponse
            {
                Status = HealthCheckStatus.Unhealthy
            };
        }

        if (restResponse.Data is null)
        {
            return new GetHealthCheckResponse
            {
                Status = HealthCheckStatus.Unknown
            };
        }
        else
        {
            try
            {
                var smtpresponse = restResponse.Data.Entries.Where(pp => pp.Key == "Email Service (Smtp)").SingleOrDefault();
                if (!string.IsNullOrEmpty(smtpresponse.Key))
                {
                    smtpresponse.Value.Status = HealthCheckStatus.Healthy;
                    var allstatus = restResponse.Data.Entries.GroupBy(pp => pp.Value.Status).ToList();
                    if (allstatus.Count() == 1)
                    {
                        if (allstatus.FirstOrDefault().Key == HealthCheckStatus.Healthy)
                        {
                            restResponse.Data.Status = HealthCheckStatus.Healthy;
                        }
                    }
                }
            }
            catch
            {

            }

        }

        return restResponse.Data;
    }
}
