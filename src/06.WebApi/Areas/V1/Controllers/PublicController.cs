using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pertamina.SolutionTemplate.Shared.Public.Constants;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleDataByKeyAndToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataStringByToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.InsertDataWithToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.UpdateDataWithToken;
using Pertamina.SolutionTemplate.Application.Public.Queries.GetDataByIDWithToken;
using Pertamina.SolutionTemplate.Application.Public.Queries.GetDataByNameWithToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetAllDataByKeyWithToken;
using Pertamina.SolutionTemplate.Application.Public.Queries.GetDatasWithToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetApplicationTypeByTokenData;
using Pertamina.SolutionTemplate.Application.Public.Commands.CreatedCatalogData;
using Pertamina.SolutionTemplate.Application.Public.Commands.UpdatedCatalogData;
using Pertamina.SolutionTemplate.Shared.Public.Queries.GetMasterDataWithToken;
using Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetMasterDataByTokenData;
using Pertamina.SolutionTemplate.Application.Public.Queries.GetTableMasterData;
using Asp.Versioning;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[ApiVersion(ApiVersioning.V1.Number)]
public class PublicController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Public.RouteTemplateFor.GetTableMasterData)]
    [Produces(typeof(OutputGetDataByTokenData))]
    public async Task<ActionResult<OutputGetMasterDataByTokenData>> GetTableMasterData([FromForm] GetMasterDataWithToken dataForm)
    {
        var output = new OutputGetMasterDataByTokenData();
        var usr = RechecktokenforMasterData(dataForm.Token, ref output);
        if (output.ResponseCode == "S")
        {
            HttpContext.User = usr;
            if (string.IsNullOrEmpty(dataForm.TableName))
            {
                dataForm.TableName = "";
            }
            else
            {
                dataForm.TableName = dataForm.TableName.ToLower();
            }

            return await Mediator.Send(new GetTableMasterDataQuery
            {
                AppValue = dataForm.TableName,
            });
        }
        else
        {
            return output;
        }
    }
    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Public.RouteTemplateFor.GetAllDataByKeyWithToken)]
    [Produces(typeof(OutputGetDataByTokenData))]
    public async Task<ActionResult<OutputGetDataByTokenData>> GetAllDataByKeyWithToken([FromForm] GetAllDataByKeyWithToken? dataForm)
    {
        var output = new OutputGetDataByTokenData();
        var usr = Rechecktoken(dataForm.Token, ref output);
        if (output.ResponseCode == "S")
        {
            HttpContext.User = usr;
            return await Mediator.Send(new GetDatasWithTokenQuery
            {
                AppKey = dataForm.Key,
                AppValue = dataForm.KeyValue,
                AppStatus = dataForm.ApplicationStatus
            });
        }
        else
        {
            return output;
        }
    }

    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Public.RouteTemplateFor.GetSingleDataByKeyWithToken)]
    [Produces(typeof(OutputGetDataByTokenData))]
    public async Task<ActionResult<OutputGetDataByTokenData>> GetSingleDataByKeyWithToken([FromForm] GetSingleDataByKeyAndToken? dataForm)
    {
        var output = new OutputGetDataByTokenData();
        if (dataForm.KeyValue.Length < 3)
        {
            output.ResponseCode = "E";
            output.ResponseMessage = "KeyValue harus di atas 3 karakter";
            output.Items = new List<GetSingleDataData>();
            output.Tanggal = System.DateTime.Now;
            return output;
        }
        else
        {
            var usr = Rechecktoken(dataForm.Token, ref output);
            if (output.ResponseCode == "S")
            {
                HttpContext.User = usr;
                if (dataForm.Key == "id")
                {
                    return await Mediator.Send(new GetDataByIDWithTokenQuery { AppID = dataForm.KeyValue.ToString(), AppStatus = dataForm.ApplicationStatus });
                }
                else if (dataForm.Key == "name")
                {
                    return await Mediator.Send(new GetDataByNameWithTokenQuery { AppNama = dataForm.KeyValue.ToString(), AppStatus = dataForm.ApplicationStatus });
                }
                else
                {
                    output.ResponseCode = "E";
                    output.ResponseMessage = "Parameter key tidak di kenal harap isi dengan id atau name";
                    output.Items = new List<GetSingleDataData>();
                    output.Tanggal = System.DateTime.Now;
                    return output;
                }
            }
            else
            {
                return output;
            }
        }
    }
    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Public.RouteTemplateFor.InsertDataWithToken)]
    [Produces(typeof(OutputGetDataStringByTokenData))]
    public async Task<ActionResult<OutputGetDataStringByTokenData>> InsertDataWithToken([FromForm] InsertDataWithToken? dataForm)
    {
        var output = new OutputGetDataStringByTokenData();
        var usr = Rechecktokenforinsert(dataForm.Token, ref output);
        if (output.ResponseCode == "S")
        {
            HttpContext.User = usr;
            if (string.IsNullOrEmpty(dataForm.Utilization))
            {
                dataForm.Utilization = "";
            }

            if (string.IsNullOrEmpty(dataForm.Application_License))
            {
                dataForm.Application_License = "";
            }

            if (!string.IsNullOrEmpty(dataForm.Application_Ats.ToString()))
            {
                if (dataForm.Application_Ats.ToString().Contains("0001"))
                {
                    dataForm.Application_Ats = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Application_Ats = System.DateTime.Now;
            }

            if (string.IsNullOrEmpty(dataForm.Application_Package))
            {
                dataForm.Application_Package = "";
            }

            if (string.IsNullOrEmpty(dataForm.User_Manual_Document))
            {
                dataForm.User_Manual_Document = "";
            }

            if (string.IsNullOrEmpty(dataForm.Link_Application))
            {
                dataForm.Link_Application = "";
            }

            if (string.IsNullOrEmpty(dataForm.Users))
            {
                dataForm.Users = "";
            }

            if (string.IsNullOrEmpty(dataForm.Criticality))
            {
                dataForm.Criticality = "";
            }

            if (string.IsNullOrEmpty(dataForm.Service_Owner))
            {
                dataForm.Service_Owner = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Server_Dev))
            {
                dataForm.Db_Server_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Name_Dev))
            {
                dataForm.Db_Name_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.App_Server_Dev))
            {
                dataForm.App_Server_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Server_Prod))
            {
                dataForm.Db_Server_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.App_Server_Prod))
            {
                dataForm.App_Server_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Name_Prod))
            {
                dataForm.Db_Name_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.Keterangan))
            {
                dataForm.Keterangan = "";
            }

            if (string.IsNullOrEmpty(dataForm.Company_Code))
            {
                dataForm.Company_Code = "";
            }

            if (!string.IsNullOrEmpty(dataForm.Start_Development.ToString()))
            {
                if (dataForm.Start_Development.ToString().Contains("0001"))
                {
                    dataForm.Start_Development = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Application_Ats = System.DateTime.Now;
            }

            if (!string.IsNullOrEmpty(dataForm.Start_Implementation.ToString()))
            {
                if (dataForm.Start_Implementation.ToString().Contains("0001"))
                {
                    dataForm.Start_Implementation = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Start_Implementation = System.DateTime.Now;
            }

            if (!string.IsNullOrEmpty(dataForm.Created_Date.ToString()))
            {
                if (dataForm.Created_Date.ToString().Contains("0001"))
                {
                    dataForm.Created_Date = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Created_Date = System.DateTime.Now;
            }

            return await Mediator.Send(new CreatedCatalogDataCommands
            {
                Application_Name = dataForm.Application_Name,
                Application_Area = dataForm.Application_Area,
                Application_Type = dataForm.Application_Type,
                Description = dataForm.Description,
                Bisnis_Process = dataForm.Bisnis_Process,
                Utilization = dataForm.Utilization,
                Application_Status = dataForm.Application_Status,
                Application_License = dataForm.Application_License,
                Application_Ats = dataForm.Application_Ats,
                Application_Package = dataForm.Application_Package,
                User_Management_Integration = dataForm.User_Management_Integration,
                User_Manual_Document = dataForm.User_Manual_Document,
                Business_Owner_Nama = dataForm.Business_Owner_Nama,
                Business_Owner_Email = dataForm.Business_Owner_Email,
                Business_Owner_KBO = dataForm.Business_Owner_KBO,
                Business_Owner_Jabatan = dataForm.Business_Owner_Jabatan,
                Business_Owner_PIC = dataForm.Business_Owner_PIC,
                Business_Owner_PIC_Email = dataForm.Business_Owner_PIC_Email,
                Developer = dataForm.Developer,
                Business_Analyst = dataForm.Business_Analyst,
                Link_Application = dataForm.Link_Application,
                Users = dataForm.Users,
                Start_Development = dataForm.Start_Development,
                Start_Implementation = dataForm.Start_Implementation,
                Criticality = dataForm.Criticality,
                Service_Owner = dataForm.Service_Owner,
                Db_Server_Dev = dataForm.Db_Server_Dev,
                Db_Name_Dev = dataForm.Db_Name_Dev,
                App_Server_Dev = dataForm.App_Server_Dev,
                Db_Server_Prod = dataForm.Db_Server_Prod,
                App_Server_Prod = dataForm.App_Server_Prod,
                Db_Name_Prod = dataForm.Db_Name_Prod,
                Keterangan = dataForm.Keterangan,
                Company_Code = dataForm.Company_Code,
                Created_Date = dataForm.Created_Date,
                Created_By = dataForm.Created_By,
            }
            );
        }
        else
        {
            return output;
        }
    }
    [AllowAnonymous]
    [HttpPost(ApiEndpoint.V1.Public.RouteTemplateFor.UpdateDataByCodeAppsWithToken)]
    [Produces(typeof(OutputGetDataStringByTokenData))]
    public async Task<ActionResult<OutputGetDataStringByTokenData>> UpdateDataByCodeAppsWithToken([FromForm] UpdateDataWithToken? dataForm)
    {
        var output = new OutputGetDataStringByTokenData();
        var usr = Rechecktokenforinsert(dataForm.Token, ref output);
        if (output.ResponseCode == "S")
        {
            HttpContext.User = usr;
            if (string.IsNullOrEmpty(dataForm.Utilization))
            {
                dataForm.Utilization = "";
            }

            if (string.IsNullOrEmpty(dataForm.Application_License))
            {
                dataForm.Application_License = "";
            }

            if (!string.IsNullOrEmpty(dataForm.Application_Ats.ToString()))
            {
                if (dataForm.Application_Ats.ToString().Contains("0001"))
                {
                    dataForm.Application_Ats = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Application_Ats = System.DateTime.Now;
            }

            if (string.IsNullOrEmpty(dataForm.Application_Package))
            {
                dataForm.Application_Package = "";
            }

            if (string.IsNullOrEmpty(dataForm.User_Manual_Document))
            {
                dataForm.User_Manual_Document = "";
            }

            if (string.IsNullOrEmpty(dataForm.Link_Application))
            {
                dataForm.Link_Application = "";
            }

            if (string.IsNullOrEmpty(dataForm.Users))
            {
                dataForm.Users = "";
            }

            if (string.IsNullOrEmpty(dataForm.Criticality))
            {
                dataForm.Criticality = "";
            }

            if (string.IsNullOrEmpty(dataForm.Service_Owner))
            {
                dataForm.Service_Owner = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Server_Dev))
            {
                dataForm.Db_Server_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Name_Dev))
            {
                dataForm.Db_Name_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.App_Server_Dev))
            {
                dataForm.App_Server_Dev = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Server_Prod))
            {
                dataForm.Db_Server_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.App_Server_Prod))
            {
                dataForm.App_Server_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.Db_Name_Prod))
            {
                dataForm.Db_Name_Prod = "";
            }

            if (string.IsNullOrEmpty(dataForm.Keterangan))
            {
                dataForm.Keterangan = "";
            }

            if (string.IsNullOrEmpty(dataForm.Company_Code))
            {
                dataForm.Company_Code = "";
            }

            if (!string.IsNullOrEmpty(dataForm.Start_Development.ToString()))
            {
                if (dataForm.Start_Development.ToString().Contains("0001"))
                {
                    dataForm.Start_Development = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Application_Ats = System.DateTime.Now;
            }

            if (!string.IsNullOrEmpty(dataForm.Start_Implementation.ToString()))
            {
                if (dataForm.Start_Implementation.ToString().Contains("0001"))
                {
                    dataForm.Start_Implementation = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Start_Implementation = System.DateTime.Now;
            }

            if (!string.IsNullOrEmpty(dataForm.Updated_Date.ToString()))
            {
                if (dataForm.Updated_Date.ToString().Contains("0001"))
                {
                    dataForm.Updated_Date = System.DateTime.Now;
                }
            }
            else
            {
                dataForm.Updated_Date = System.DateTime.Now;
            }

            return await Mediator.Send(new UpdatedCatalogDataCommands
            {
                Code_Apps = dataForm.Code_Apps,
                Application_Name = dataForm.Application_Name,
                Application_Area = dataForm.Application_Area,
                Application_Type = dataForm.Application_Type,
                Description = dataForm.Description,
                Bisnis_Process = dataForm.Bisnis_Process,
                Utilization = dataForm.Utilization,
                Application_Status = dataForm.Application_Status,
                Application_License = dataForm.Application_License,
                Application_Ats = dataForm.Application_Ats,
                Application_Package = dataForm.Application_Package,
                User_Management_Integration = dataForm.User_Management_Integration,
                User_Manual_Document = dataForm.User_Manual_Document,
                Business_Owner_Nama = dataForm.Business_Owner_Nama,
                Business_Owner_Email = dataForm.Business_Owner_Email,
                Business_Owner_KBO = dataForm.Business_Owner_KBO,
                Business_Owner_Jabatan = dataForm.Business_Owner_Jabatan,
                Business_Owner_PIC = dataForm.Business_Owner_PIC,
                Business_Owner_PIC_Email = dataForm.Business_Owner_PIC_Email,
                Developer = dataForm.Developer,
                Business_Analyst = dataForm.Business_Analyst,
                Link_Application = dataForm.Link_Application,
                Users = dataForm.Users,
                Start_Development = dataForm.Start_Development,
                Start_Implementation = dataForm.Start_Implementation,
                Criticality = dataForm.Criticality,
                Service_Owner = dataForm.Service_Owner,
                Db_Server_Dev = dataForm.Db_Server_Dev,
                Db_Name_Dev = dataForm.Db_Name_Dev,
                App_Server_Dev = dataForm.App_Server_Dev,
                Db_Server_Prod = dataForm.Db_Server_Prod,
                App_Server_Prod = dataForm.App_Server_Prod,
                Db_Name_Prod = dataForm.Db_Name_Prod,
                Keterangan = dataForm.Keterangan,
                Company_Code = dataForm.Company_Code,
                Updated_Date = dataForm.Updated_Date,
                Updated_By = dataForm.Updated_By,
            }
            );
        }
        else
        {
            return output;
        }
    }
    private static ClaimsPrincipal Rechecktoken(string token, ref OutputGetDataByTokenData rtnoutput)
    {
        var usr = new ClaimsPrincipal();
        try
        {
            if (!string.IsNullOrEmpty(token))
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var bacces = false;
                var validto = jwt.ValidTo.AddHours(+7);
                var lapi = new List<string>
                {
                    "50327805-bc3a-4fe1-aa00-ff6633daa5cf/kpiapplicationcatalog.access",
                    "8b7fc1be-4952-4344-8417-677b00915979/kpiapplicationcatalog.access"
                };
                try
                {
                    foreach (var strapi in lapi)
                    {
                        try
                        {
                            var foundclaim = jwt.Claims.Where(pp => pp.Value.Contains(strapi)).ToList();
                            if (foundclaim.Any())
                            {
                                bacces = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                catch (Exception ex)
                {

                }

                if (bacces)
                {
                    var dtnow = System.DateTime.Now;
                    if (dtnow <= validto)
                    {
                        var claims = new List<Claim>();

                        foreach (var claim in jwt.Claims)
                        {
                            if (claim.Type
                                is OidcConstants.TokenResponse.AccessToken
                                or OidcConstants.TokenResponse.RefreshToken
                                or AuthorizationClaimTypes.PositionId
                                or AuthorizationClaimTypes.PositionName
                                or AuthorizationClaimTypes.Permission)
                            {
                                continue;
                            }
                            else if (claim.Type.StartsWith(AuthorizationClaimTypes.CustomParameter))
                            {
                                continue;
                            }
                            else
                            {
                                claims.Add(claim);
                            }
                        }

                        var noneAuthorizationIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        usr = new ClaimsPrincipal(noneAuthorizationIdentity);
                        rtnoutput.ResponseCode = "S";
                        rtnoutput.ResponseMessage = "Sukses";
                        rtnoutput.Items = new List<GetSingleDataData>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                    else
                    {
                        rtnoutput.ResponseCode = "E";
                        rtnoutput.ResponseMessage = "Token expire";
                        rtnoutput.Items = new List<GetSingleDataData>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                }
                else
                {
                    rtnoutput.ResponseCode = "E";
                    rtnoutput.ResponseMessage = "Scope tidak dikenal";
                    rtnoutput.Items = new List<GetSingleDataData>();
                    rtnoutput.Tanggal = System.DateTime.Now;
                }

            }
            else
            {
                rtnoutput.ResponseCode = "E";
                rtnoutput.ResponseMessage = "Token expire";
                rtnoutput.Items = new List<GetSingleDataData>();
                rtnoutput.Tanggal = System.DateTime.Now;
            }

        }
        catch (Exception ex)
        {
            rtnoutput.ResponseCode = "E";
            if (ex.StackTrace.Count() >= 200)
            {
                rtnoutput.ResponseMessage = ex.StackTrace[..200];
            }
            else
            {
                rtnoutput.ResponseMessage = ex.StackTrace;
            }

            rtnoutput.Tanggal = System.DateTime.Now;
            rtnoutput.Items = new List<GetSingleDataData>();
        }

        return usr;
    }
    private static ClaimsPrincipal Rechecktokenforinsert(string token, ref OutputGetDataStringByTokenData rtnoutput)
    {
        var usr = new ClaimsPrincipal();
        try
        {
            if (!string.IsNullOrEmpty(token))
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var bacces = false;
                var validto = jwt.ValidTo.AddHours(+7);
                var lapi = new List<string>
                {
                    "50327805-bc3a-4fe1-aa00-ff6633daa5cf/kpiapplicationcatalog.access",
                    "8b7fc1be-4952-4344-8417-677b00915979/kpiapplicationcatalog.access"
                };
                try
                {
                    foreach (var strapi in lapi)
                    {
                        try
                        {
                            var foundclaim = jwt.Claims.Where(pp => pp.Value.Contains(strapi)).ToList();
                            if (foundclaim.Any())
                            {
                                bacces = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                catch (Exception ex)
                {

                }

                if (bacces)
                {
                    var dtnow = System.DateTime.Now;
                    if (dtnow <= validto)
                    {
                        var claims = new List<Claim>();

                        foreach (var claim in jwt.Claims)
                        {
                            if (claim.Type
                                is OidcConstants.TokenResponse.AccessToken
                                or OidcConstants.TokenResponse.RefreshToken
                                or AuthorizationClaimTypes.PositionId
                                or AuthorizationClaimTypes.PositionName
                                or AuthorizationClaimTypes.Permission)
                            {
                                continue;
                            }
                            else if (claim.Type.StartsWith(AuthorizationClaimTypes.CustomParameter))
                            {
                                continue;
                            }
                            else
                            {
                                claims.Add(claim);
                            }
                        }

                        var noneAuthorizationIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        usr = new ClaimsPrincipal(noneAuthorizationIdentity);
                        rtnoutput.ResponseCode = "S";
                        rtnoutput.ResponseMessage = "Sukses";
                        rtnoutput.Items = "";
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                    else
                    {
                        rtnoutput.ResponseCode = "E";
                        rtnoutput.ResponseMessage = "Token Expire";
                        rtnoutput.Items = "";
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                }
                else
                {
                    rtnoutput.ResponseCode = "E";
                    rtnoutput.ResponseMessage = "Scope tidak dikenal";
                    rtnoutput.Items = "";
                    rtnoutput.Tanggal = System.DateTime.Now;
                }

            }
            else
            {
                rtnoutput.ResponseCode = "E";
                rtnoutput.ResponseMessage = "Token expire";
                rtnoutput.Items = "";
                rtnoutput.Tanggal = System.DateTime.Now;
            }

        }
        catch (Exception ex)
        {
            rtnoutput.ResponseCode = "E";
            if (ex.StackTrace.Count() >= 200)
            {
                rtnoutput.ResponseMessage = ex.StackTrace[..200];
            }
            else
            {
                rtnoutput.ResponseMessage = ex.StackTrace;
            }

            rtnoutput.Tanggal = System.DateTime.Now;
            rtnoutput.Items = "";
        }

        return usr;
    }
    private static ClaimsPrincipal RechecktokenforMasterData(string token, ref OutputGetMasterDataByTokenData rtnoutput)
    {
        var usr = new ClaimsPrincipal();
        try
        {
            if (!string.IsNullOrEmpty(token))
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var bacces = false;
                var validto = jwt.ValidTo.AddHours(+7);
                var lapi = new List<string>
                {
                    "50327805-bc3a-4fe1-aa00-ff6633daa5cf/kpiapplicationcatalog.access",
                    "8b7fc1be-4952-4344-8417-677b00915979/kpiapplicationcatalog.access"
                };
                try
                {
                    foreach (var strapi in lapi)
                    {
                        try
                        {
                            var foundclaim = jwt.Claims.Where(pp => pp.Value.Contains(strapi)).ToList();
                            if (foundclaim.Any())
                            {
                                bacces = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                catch (Exception ex)
                {

                }

                if (bacces)
                {
                    var dtnow = System.DateTime.Now;
                    if (dtnow <= validto)
                    {
                        var claims = new List<Claim>();

                        foreach (var claim in jwt.Claims)
                        {
                            if (claim.Type
                                is OidcConstants.TokenResponse.AccessToken
                                or OidcConstants.TokenResponse.RefreshToken
                                or AuthorizationClaimTypes.PositionId
                                or AuthorizationClaimTypes.PositionName
                                or AuthorizationClaimTypes.Permission)
                            {
                                continue;
                            }
                            else if (claim.Type.StartsWith(AuthorizationClaimTypes.CustomParameter))
                            {
                                continue;
                            }
                            else
                            {
                                claims.Add(claim);
                            }
                        }

                        var noneAuthorizationIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        usr = new ClaimsPrincipal(noneAuthorizationIdentity);
                        rtnoutput.ResponseCode = "S";
                        rtnoutput.ResponseMessage = "Sukses";
                        rtnoutput.Items = new List<Shared.Public.Queries.GetSingleMasterData.GetSingleMasterDataData>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                    else
                    {
                        rtnoutput.ResponseCode = "E";
                        rtnoutput.ResponseMessage = "Token expire";
                        rtnoutput.Items = new List<Shared.Public.Queries.GetSingleMasterData.GetSingleMasterDataData>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                }
                else
                {
                    rtnoutput.ResponseCode = "E";
                    rtnoutput.ResponseMessage = "Scope tidak dikenal";
                    rtnoutput.Items = new List<Shared.Public.Queries.GetSingleMasterData.GetSingleMasterDataData>();
                    rtnoutput.Tanggal = System.DateTime.Now;
                }

            }
            else
            {
                rtnoutput.ResponseCode = "E";
                rtnoutput.ResponseMessage = "Token expire";
                rtnoutput.Items = new List<Shared.Public.Queries.GetSingleMasterData.GetSingleMasterDataData>();
                rtnoutput.Tanggal = System.DateTime.Now;
            }

        }
        catch (Exception ex)
        {
            rtnoutput.ResponseCode = "E";
            if (ex.StackTrace.Count() >= 200)
            {
                rtnoutput.ResponseMessage = ex.StackTrace[..200];
            }
            else
            {
                rtnoutput.ResponseMessage = ex.StackTrace;
            }

            rtnoutput.Tanggal = System.DateTime.Now;
            rtnoutput.Items = new List<Shared.Public.Queries.GetSingleMasterData.GetSingleMasterDataData>();
        }

        return usr;
    }
    private static ClaimsPrincipal RechecktokenforApplicationType(string token, ref OutputGetApplicationTypeByTokenData rtnoutput)
    {
        var usr = new ClaimsPrincipal();
        try
        {
            if (!string.IsNullOrEmpty(token))
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var bacces = false;
                var validto = jwt.ValidTo.AddHours(+7);
                var lapi = new List<string>
                {
                    "50327805-bc3a-4fe1-aa00-ff6633daa5cf/kpiapplicationcatalog.access",
                    "8b7fc1be-4952-4344-8417-677b00915979/kpiapplicationcatalog.access"
                };
                try
                {
                    foreach (var strapi in lapi)
                    {
                        try
                        {
                            var foundclaim = jwt.Claims.Where(pp => pp.Value.Contains(strapi)).ToList();
                            if (foundclaim.Any())
                            {
                                bacces = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                catch (Exception ex)
                {

                }

                if (bacces)
                {
                    var dtnow = System.DateTime.Now;
                    if (dtnow <= validto)
                    {
                        var claims = new List<Claim>();

                        foreach (var claim in jwt.Claims)
                        {
                            if (claim.Type
                                is OidcConstants.TokenResponse.AccessToken
                                or OidcConstants.TokenResponse.RefreshToken
                                or AuthorizationClaimTypes.PositionId
                                or AuthorizationClaimTypes.PositionName
                                or AuthorizationClaimTypes.Permission)
                            {
                                continue;
                            }
                            else if (claim.Type.StartsWith(AuthorizationClaimTypes.CustomParameter))
                            {
                                continue;
                            }
                            else
                            {
                                claims.Add(claim);
                            }
                        }

                        var noneAuthorizationIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        usr = new ClaimsPrincipal(noneAuthorizationIdentity);
                        rtnoutput.ResponseCode = "S";
                        rtnoutput.ResponseMessage = "Sukses";
                        rtnoutput.Items = new List<Shared.Public.Queries.GetSingleDataApplicationType.GetSingleDataApplicationType>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                    else
                    {
                        rtnoutput.ResponseCode = "E";
                        rtnoutput.ResponseMessage = "Token expire";
                        rtnoutput.Items = new List<Shared.Public.Queries.GetSingleDataApplicationType.GetSingleDataApplicationType>();
                        rtnoutput.Tanggal = System.DateTime.Now;
                    }
                }
                else
                {
                    rtnoutput.ResponseCode = "E";
                    rtnoutput.ResponseMessage = "Scope tidak dikenal";
                    rtnoutput.Items = new List<Shared.Public.Queries.GetSingleDataApplicationType.GetSingleDataApplicationType>();
                    rtnoutput.Tanggal = System.DateTime.Now;
                }

            }
            else
            {
                rtnoutput.ResponseCode = "E";
                rtnoutput.ResponseMessage = "Token expire";
                rtnoutput.Items = new List<Shared.Public.Queries.GetSingleDataApplicationType.GetSingleDataApplicationType>();
                rtnoutput.Tanggal = System.DateTime.Now;
            }

        }
        catch (Exception ex)
        {
            rtnoutput.ResponseCode = "E";
            if (ex.StackTrace.Count() >= 200)
            {
                rtnoutput.ResponseMessage = ex.StackTrace[..200];
            }
            else
            {
                rtnoutput.ResponseMessage = ex.StackTrace;
            }

            rtnoutput.Tanggal = System.DateTime.Now;
            rtnoutput.Items = new List<Shared.Public.Queries.GetSingleDataApplicationType.GetSingleDataApplicationType>();
        }

        return usr;
    }
}

