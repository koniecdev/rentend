using Microsoft.AspNetCore.Mvc;
using rentend.Models;
using System.Diagnostics;

namespace rentend.Home.Controllers
{
	[Area("Home")]
	public class LoginController : Controller
	{
		public LoginController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}