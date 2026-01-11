using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Application.Racks.Queries.GetRacks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[Area("V1")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RacksController : ApiControllerBase
{
    // GET: api/v1/racks
    // Endpoint ini dipanggil oleh InventoryViewModel.LoadRacksAsync()
    [HttpGet]
    [AllowAnonymous] // Biar bisa dites tanpa login dulu
    public async Task<ActionResult<List<RackDto>>> GetRacks()
    {
        return await Mediator.Send(new GetRacksQuery());
    }
}