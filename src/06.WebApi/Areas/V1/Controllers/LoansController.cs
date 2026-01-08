using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Application.Loans.Commands.Create;
using Pertamina.SolutionTemplate.Application.Loans.Commands.Update;
using Pertamina.SolutionTemplate.Application.Loans.Queries.GetLoanById;
using Pertamina.SolutionTemplate.WebApi.Areas.V1.Controllers;

namespace Pertamina.SolutionTemplate.WebAPI.Areas.V1.Controllers;

public class LoansController : ApiControllerBase
{
    // POST: api/v1/loans
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateLoanCommand command)
    {
        return await Mediator.Send(command);
    }

    // GET: api/v1/loans/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanTransaction?>> Get(Guid id)
    {
        return await Mediator.Send(new GetLoanByIdQuery(id));
    }

    // PUT: api/v1/loans/status
    [HttpPut("status")]
    public async Task<ActionResult<bool>> UpdateStatus(UpdateLoanStatusCommand command)
    {
        return await Mediator.Send(command);
    }
}
