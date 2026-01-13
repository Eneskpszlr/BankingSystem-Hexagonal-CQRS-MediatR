using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.PrimaryPorts.UserPorts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public AccountsController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = _currentUserService.GetUserId();
            bool isAdmin = User.IsInRole("Admin");

            var query = new GetAccountsQuery
            {
                UserId = userId,
                IsAdmin = isAdmin
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // 1. İsteği yapan kim?
            int userId = _currentUserService.GetUserId();

            var query = new GetAccountByIdQuery(id)
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound("Hesap bulunamadı.");

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
        {
            var isAdmin = User.IsInRole("Admin");

            if (isAdmin)
            {
                if (command.CustomerId == 0)
                    return BadRequest("Admin işlemi için CustomerId girmek zorunludur.");
            }
            else
            {
                command.CustomerId = _currentUserService.GetUserId();
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.EntityId }, result);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveAccountCommand { Id = id });

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
