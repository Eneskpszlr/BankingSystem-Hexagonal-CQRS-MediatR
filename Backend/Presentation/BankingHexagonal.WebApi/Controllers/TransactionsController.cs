using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.PrimaryPorts.UserPorts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public TransactionsController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTransactionsQuery query)
        {
            query.UserId = _currentUserService.GetUserId();

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTransactionByIdQuery(id));
            if (result == null) 
                return NotFound("İşlem bulunamadı.");
            return Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositTransactionCommand command)
        {
            command.UserId = _currentUserService.GetUserId();

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawTransactionCommand command)
        {
            command.UserId = _currentUserService.GetUserId();

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferTransactionCommand command)
        {
            command.UserId = _currentUserService.GetUserId();

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            // Transfer sonucunda ReferenceNumber dönüyoruz, bu result içinde var.
            return Ok(result);
        }
    }
}
