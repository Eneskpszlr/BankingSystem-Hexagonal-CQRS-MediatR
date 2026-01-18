using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.PrimaryPorts.UserPorts;
using BankingHexagonal.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CustomersController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCustomersQuery());
            return Ok(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            // ID'yi dışarıdan istemiyoruz, Token'dan alıyoruz.
            int myCustomerId = _currentUserService.GetUserId();

            var result = await _mediator.Send(new GetCustomerByIdQuery(myCustomerId));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerByIdQuery(id)
            {
                UserId = _currentUserService.GetUserId()
            };
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result == null) 
                return NotFound("Müşteri bulunamadı.");
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.EntityId }, result);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
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
            var result = await _mediator.Send(new RemoveCustomerCommand { Id = id });

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
