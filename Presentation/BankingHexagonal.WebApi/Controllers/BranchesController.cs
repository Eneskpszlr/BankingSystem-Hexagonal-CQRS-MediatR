using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetBranchByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveBranchCommand(id));
            return Ok(result);
        }
    }

}
