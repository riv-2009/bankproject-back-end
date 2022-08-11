using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Identity;
using Domain;
using Domain.DTO;

namespace Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAccountService _accountService;
		private readonly MyDbContext _dbContext;
		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountService accountService, MyDbContext dbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_accountService = accountService;
			_dbContext = dbContext;
		}

		public async Task<ApplicationUser> SignInAsync(SignIn signIn)
		{
			var user = await _userManager.FindByNameAsync(signIn.UserName);
			var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, false, false);
			if(!result.Succeeded) {
				throw new Exception("user could not sign in");
			}
			return user;
		}

		public async Task SignOutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<ApplicationUser> CreateUserAsync(User user)
		{
			var result = await _userManager.CreateAsync(new ApplicationUser { UserName = user.UserName }, user.Password);
			var retrieved = await _userManager.FindByNameAsync(user.UserName);
			var checkingAccount = new Account();
			checkingAccount.Balance = 0;
			checkingAccount.Deleted = 0;
			checkingAccount.Type = 1;
			checkingAccount.UserId = new Guid(retrieved.Id);
			await _accountService.CreateAccount(checkingAccount);
			var savingsAccount = new Account();
			savingsAccount.Balance = 0;
			savingsAccount.Deleted = 0;
			savingsAccount.Type = 2;
			savingsAccount.UserId = new Guid(retrieved.Id);
			await _accountService.CreateAccount(savingsAccount);
			var moneyAccount = new Account();
			moneyAccount.Balance = 0;
			moneyAccount.Deleted = 0;
			moneyAccount.Type = 3;
			moneyAccount.UserId = new Guid(retrieved.Id);
			await _accountService.CreateAccount(moneyAccount);
			await _dbContext.SaveChangesAsync();
			return retrieved;
		}
		public async Task<bool> CheckUserExists(String userName)
		{
			var result = await _userManager.FindByNameAsync(userName);
			if (result == null) {return false;} return true;
		}
	}
}