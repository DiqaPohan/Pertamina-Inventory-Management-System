using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Shared.Data.Constants;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetToken;
using Pertamina.SolutionTemplate.Shared.Data.Queries.OutputToken;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[ApiVersion(ApiVersioning.V1.Number)]
public class AuthController : ApiControllerBase
{

    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Data.RouteTemplateFor.GetToken)]
    [Produces(typeof(OutputTokenData))]
    public ActionResult<OutputTokenData> GetToken([FromForm] GetTokenForm tokenForm)
    {
        var outputdata = new OutputTokenData();

        if (!ModelState.IsValid)
        {
            outputdata.ResponseCode = "E";
            outputdata.ResponseMessage = "parameter masih kosong";
            outputdata.Items = "";
            outputdata.Tanggal = System.DateTime.Now;
        }
        else
        {
            if (tokenForm.ClientScope.Contains("#"))
            {

            }
            else if (tokenForm.ClientScope.Contains("#"))
            {

            }
            else
            {
                outputdata.ResponseCode = "E";
                outputdata.ResponseMessage = "parameter client scope tidak di kenal";
                outputdata.Items = "";
                outputdata.Tanggal = System.DateTime.Now;
            }

        }

        return outputdata;
    }

}

