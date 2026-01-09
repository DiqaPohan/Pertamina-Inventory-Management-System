using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.DeleteItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItemById;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItems;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[Area("V1")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ItemsController : ApiControllerBase
{
    // READ ALL
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<Item>>> GetItems([FromQuery] GetItemsQuery query)
    {
        // Mediator sekarang akan mengembalikan PaginatedListResponse<Item>
        return await Mediator.Send(query);
    }

    // READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        var result = await Mediator.Send(new GetItemByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    // CREATE
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateItemCommand command)
    {
        try
        {
            // Validasi manual sederhana biar tidak crash db
            if (string.IsNullOrEmpty(command.Name))
                return BadRequest("Nama barang tidak boleh kosong.");

            // Mediator sekarang mengembalikan Guid (sesuai kode CreateItemCommandHandler mu)
            var resultId = await Mediator.Send(command);

            return Ok(resultId);
        }
        catch (Exception ex)
        {
            // Tangkap error biar debug TIDAK BERHENTI (Crash)
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateItemCommand command)
    {
        if (id != command.Id) return BadRequest();

        var result = await Mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteItemCommand(id));
        return result ? NoContent() : NotFound();
    }
}