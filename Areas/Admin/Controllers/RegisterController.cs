namespace rentend.Admin.Controllers;

[Area("Admin")]
public class RegisterController : Controller
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;
	public RegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}
	
	public IActionResult Index(){
		return View(new RegisterViewModel());
	}
	
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Index(RegisterViewModel user){
		if(user.Password != user.ConfirmPassword){
			TempData["error"] = "Passwords are not the same";
			return View(user);
		}
		IdentityUser newUser = new(){
			UserName = user.Username,
			Email = user.Email
		};
		var r = await _userManager.CreateAsync(newUser, user.Password);
		if(r != IdentityResult.Success){
			TempData["error"] = r.Errors;
			return View(user);
		}
		return RedirectToAction(controllerName: "Admin", actionName: "Index");
	}
}