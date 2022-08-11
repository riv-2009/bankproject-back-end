using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;
using Domain;

namespace Controllers
{
	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost]
		[Route("signin")]
		public async Task<IActionResult> SignIn([FromBody]SignIn signIn)
		{
			ApplicationUser retrievedUser = null;
			try
			{
				retrievedUser = await _authenticationService.SignInAsync(signIn);
				if (retrievedUser == null)
				{
					return Unauthorized();
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
				return Unauthorized();
			}
			return Ok(retrievedUser.Id);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create([FromBody] User user)
		{
			var retrievedUser = await _authenticationService.CreateUserAsync(user);
			return Ok(retrievedUser.Id);
		}

		[HttpPost]
		// [ValidateAntiForgeryToken]
		[Route("signout")]
		public async Task<IActionResult> SignOut()
		{
			await _authenticationService.SignOutAsync();
			return Ok();
		}

		[HttpPost]
		[Route("{username}")]
		public async Task<IActionResult> CheckUserExists([FromRoute] String userName){
			try{
				return Ok(await _authenticationService.CheckUserExists(userName));
			}catch (Exception e){
				Console.WriteLine(e);
				return StatusCode(500, "");
			}
		}
	}
}