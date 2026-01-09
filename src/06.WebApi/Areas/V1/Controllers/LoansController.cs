using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using Pertamina.SolutionTemplate.Application.Loans.Commands.Create;
using Pertamina.SolutionTemplate.Application.Loans.Commands.Update;
using Pertamina.SolutionTemplate.Application.LoanTransactions.Queries.GetLoanById;
using Pertamina.SolutionTemplate.Application.Loans.Queries.GetLoans;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

[Area("V1")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[AllowAnonymous] // Menambahkan akses anonim untuk seluruh isi Controller ini
public class LoansController : ApiControllerBase
{
    // READ ALL (PAGINATED)
    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<LoanTransaction>>> GetLoans([FromQuery] GetLoansQuery query)
    {
        return await Mediator.Send(query);
    }

    // READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanTransaction>> GetLoan(Guid id)
    {
        var result = await Mediator.Send(new GetLoanByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateLoanCommand command)
    {
        try
        {
            var resultId = await Mediator.Send(command);
            return Ok(resultId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // UPDATE STATUS
    [HttpPut("status/{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateLoanStatusCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch");

        var result = await Mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}