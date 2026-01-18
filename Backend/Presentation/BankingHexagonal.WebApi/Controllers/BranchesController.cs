using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers;
using BankingHexagonal.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetBranchesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetBranchByIdQuery(id));
            if (result == null) 
                return NotFound("Şube bulunamadı.");
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateBranchCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.EntityId }, result);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update([FromBody] UpdateBranchCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveBranchCommand { Id = id });

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }

}
