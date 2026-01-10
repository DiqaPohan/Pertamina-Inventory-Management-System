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
[AllowAnonymous]
public class LoansController : ApiControllerBase
{
    // READ ALL (PAGINATED)
    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<LoanTransaction>>> GetLoans([FromQuery] GetLoansQuery query)
    {
        // Pastikan di Query Handler lu udah pake .AsNoTracking() biar gak berat
        return await Mediator.Send(query);
    }

    // READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanTransaction>> GetLoan(Guid id)
    {
        var result = await Mediator.Send(new GetLoanByIdQuery(id));

        // Tambahin pesan error yang jelas biar enak di-trace
        if (result == null) return NotFound($"Transaksi peminjaman dengan ID {id} tidak ditemukan.");

        return Ok(result);
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateLoanCommand command)
    {
        try
        {
            // Tambahin validasi dasar di sini atau di FluentValidation
            if (command.ItemId == Guid.Empty) return BadRequest("ItemId wajib diisi.");

            var resultId = await Mediator.Send(command);
            return Ok(resultId);
        }
        catch (Exception ex)
        {
            // Catch error dari Handler (seperti stok habis atau status barang bukan Active)
            return BadRequest(ex.Message);
        }
    }

    // UPDATE STATUS (Return / Cancel / dll)
    [HttpPut("status/{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateLoanStatusCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch antara URL dan Body.");

        try
        {
            var result = await Mediator.Send(command);
            return result ? NoContent() : NotFound("Transaksi tidak ditemukan.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}