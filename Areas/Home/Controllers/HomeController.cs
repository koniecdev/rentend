using Microsoft.AspNetCore.Mvc;
using rentend.Data;
using rentend.Models;
using rentend.Models.ViewModels;
using System.Diagnostics;

namespace rentend.Home.Controllers
{
	[Area("Home")]
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _db;
		public HomeController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			HomeViewModel mdel = new(){IndexVM = new()};
			mdel.Departaments = await _db.Departaments.ToListAsync();
			var pins = await _db.Pins.Include(m=>m.Car).ThenInclude(m=>m.Brand).ToListAsync();
			foreach(var pin in pins){
				int carid = pin.CarId;
				var file = Directory.GetFiles(Path.Combine("wwwroot/img", carid.ToString())).FirstOrDefault();
				if(file != null){
					var tpl = new Tuple<Car, string>(pin.Car, file);
					mdel.Cars.Add(tpl);
				}
			}
			return View(mdel);
		}
	}
}