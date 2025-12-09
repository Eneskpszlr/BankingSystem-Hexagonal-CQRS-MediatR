using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingHexagonal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetTransactionsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetTransactionByIdQuery(id));
            return Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
