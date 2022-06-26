namespace rentend.Admin.Controllers;

[Area("Home")]
public class LoginController : Controller
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;
	public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public IActionResult Index(){
		return View(new UserViewModel());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Index(UserViewModel user){
		var x = await _userManager.FindByEmailAsync(user.Email);
		if(x != null){
			await _signInManager.PasswordSignInAsync(x, user.Password, user.Remember, false);
		}
		return RedirectToAction();
	}

	public async Task<IActionResult> Logout(){
		await _signInManager.SignOutAsync();
		return RedirectToAction(nameof(Index));
	}
}