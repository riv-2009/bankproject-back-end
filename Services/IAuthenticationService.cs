using System.Threading.Tasks;
using Domain;
using Domain.DTO;

namespace Services
{
	public interface IAuthenticationService
	{
		Task<ApplicationUser> SignInAsync(SignIn signIn);
		Task SignOutAsync();
		Task<ApplicationUser> CreateUserAsync(User user);
		Task<bool> CheckUserExists(String userName);
	}
}