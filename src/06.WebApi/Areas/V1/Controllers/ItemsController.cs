using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Application.Items.Commands.CreateItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.DeleteItem;
using Pertamina.SolutionTemplate.Application.Items.Commands.UpdateItem;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItems;
using Pertamina.SolutionTemplate.Application.Items.Queries.GetItemById;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

public class ItemsController : ApiControllerBase
{
    // READ ALL
    [HttpGet]
    public async Task<ActionResult<List<Item>>> GetItems()
    {
        return await Mediator.Send(new GetItemsQuery());
    }

    // READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        var result = await Mediator.Send(new GetItemByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateItemCommand command)
    {
        return await Mediator.Send(command);
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