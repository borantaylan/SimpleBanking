using Banking.Domain.Queries;
using Banking.WebAPI.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Domain.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediatr;
        private readonly IAccountQueries _accountQueries;

        public AccountsController(ILogger<AccountsController> logger, IMediator mediatr, IAccountQueries accountQueries)
        {
            _logger = logger;
            _mediatr = mediatr;
            _accountQueries = accountQueries;
        }

        [Route("{accountNumber}")]
        [HttpGet]
        public async Task<IActionResult> GetAccount([FromRoute]string accountNumber)
        {
                var account = await _accountQueries.GetAccount(accountNumber);
                return Ok(account);
        }

        [Route("users/{userId:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromRoute] Guid userId)
        {
            var accounts = await _accountQueries.GetAccountByUserId(userId);
            return Ok(accounts);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand createAccountCommand)
        {
            return Ok(await _mediatr.Send(createAccountCommand));
        }

        [Route("deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand depositCommand)
        {
            await _mediatr.Send(depositCommand);
            return Ok();
        }

        [Route("withdraw")]
        [HttpPost]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand depositCommand)
        {
            await _mediatr.Send(depositCommand);
            return Ok();
        }

        [Route("delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountCommand deleteAccountCommand)
        {
            await _mediatr.Send(deleteAccountCommand);
            return Ok();
        }

        [Route("deleteAll")]
        [HttpPost]
        public async Task<IActionResult> DeleteAllAccounts([FromBody] DeleteAllAccountsCommand deleteAccountCommand)
        {
            await _mediatr.Send(deleteAccountCommand);
            return Ok();
        }
    }
}