using System.Threading.Tasks;
using Domain;
using Domain.DTO;
using Services;
using Microsoft.AspNetCore.Mvc;
namespace Controllers;

// localhost:5000/api/account POST

[Route("api/[controller]")]
public class AccountController : Controller {

    private IAccountService _accountService;

    public AccountController(IAccountService accountService) {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task CreateAccount([FromBody]Account account) {
        try {
            await _accountService.CreateAccount(account);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
    }

    [HttpPost]
    [Route("tranfer/")]
    public async Task TransferBalance([FromBody]Transfer transfer) 
    {
        try {
            await _accountService.TransferBalance(transfer);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
    }
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetAccountsByUser(Guid userId) {
        try {
            return Ok(await _accountService.GetAccountByUserId(userId));
        } catch (Exception e) {
            return StatusCode(400, e);
        }
    }

    [HttpPatch]
    [Route("balance")]
    public async Task<IActionResult> UpdateAccountBalance([FromBody]Balance balance) {
        try {
            await _accountService.UpdateAccountBalance(balance.Amount, balance.AccountId, balance.UserId, balance.AccountType);
            return Ok();
        } catch (Exception e) {
            return StatusCode(400, e);
        }
    }

    [HttpPut]
    [Route("{accountId}")]
    public async Task<IActionResult> UpdateAccountData([FromBody]Account account) {
        try {
            await _accountService.UpdateAccountData(account);
            return Ok();
        } catch (Exception e) {
            return StatusCode(400, e);
        }
    }

    [HttpDelete]
    [Route("{accountId}")]
    public async Task<IActionResult> DeleteAccount([FromRoute]int accountId) {
        try {
            await _accountService.DeleteAccount(accountId);
            return Ok();
        } catch (Exception e) {
            return StatusCode(400, e);
        }
    }
    
    [HttpGet]
    [Route("type")]
    public async Task<IActionResult> GetAccountTypes() {
        try {
            return Ok(await _accountService.GetAccountTypes());
        } catch (Exception e) {
            return StatusCode(400, e);
        }
    } 
}