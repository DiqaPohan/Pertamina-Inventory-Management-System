using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.DeleteItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.ConfirmPlacement;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItemById;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItems;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetPendingItems;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[Area("V1")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ItemsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<Item>>> GetItems([FromQuery] GetItemsQuery query)
    {
        return await Mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpGet("pending")]
    public async Task<ActionResult<List<PendingItemDto>>> GetPendingItems()
    {
        return await Mediator.Send(new GetPendingItemsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        var result = await Mediator.Send(new GetItemByIdQuery(id));

        // Ini sudah bagus, tapi pastikan "result" isinya Item yang sudah include Rack
        if (result == null) return NotFound($"Barang dengan ID {id} tidak ditemukan.");

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateItemCommand command)
    {
        try
        {
            if (string.IsNullOrEmpty(command.Name))
                return BadRequest("Nama barang tidak boleh kosong.");

            var resultId = await Mediator.Send(command);
            return Ok(resultId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // UPDATE DI SINI: Sekarang butuh rackId dari body/query
    [AllowAnonymous]
    [HttpPut("{id}/confirm-placement")]
    public async Task<ActionResult<bool>> ConfirmPlacement(Guid id, [FromQuery] string rackId)
    {
        // SEKARANG KIRIM DUA ARGUMEN: id barang dan rackId hasil scan
        var result = await Mediator.Send(new ConfirmPlacementCommand(id, rackId));
        return result ? Ok(result) : NotFound("Barang gagal diaktifkan.");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateItemCommand command)
    {
        if (id != command.Id) return BadRequest();

        var result = await Mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteItemCommand(id));
        return result ? NoContent() : NotFound();
    }
}