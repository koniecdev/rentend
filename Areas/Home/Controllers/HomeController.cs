using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rentend.Data;
using rentend.Models;
using rentend.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace rentend.Home.Controllers
{
	[Area("Home")]
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public async Task<IActionResult> Index()
		{
			HomeViewModel mdel = new();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Departments"))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						mdel.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			List<Pin>? pins = new();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Pins"))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						pins = JsonConvert.DeserializeObject<List<Pin>>(apiResponse);
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			foreach (var pin in pins){
				int carid = pin.CarId;
				var file = Directory.GetFiles(Path.Combine("wwwroot/img", carid.ToString())).FirstOrDefault();
				if(file != null){
					var tpl = new Tuple<Car, string>(pin.Car, file);
					mdel.Cars.Add(tpl);
				}
			}
			return View(mdel);
		}

		//[HttpPost, ActionName("Index")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> IndexPost(HomeViewModel mdel){
		//	List<Car> CarsFromCity = await _db.Cars.Where(m=>m.DepartamentId.Equals(mdel.IndexVM.DepartmentId)).ToListAsync();
		//	var Booked = await _db.Rents.Include(m=>m.Car).Where(m=>m.Since>=mdel.IndexVM.RentSince && m.Until<=mdel.IndexVM.RentTo && m.Car.DepartamentId.Equals(mdel.IndexVM.DepartmentId)).ToListAsync();
		//	List<Car> NotAvailableCars = new();
		//	List<Car> Availables = new();
		//	foreach(var car in Booked){
		//		NotAvailableCars.Add(car.Car);
		//	}
		//	Availables = CarsFromCity.Except(NotAvailableCars).ToList();
		//	foreach(var car in Availables){
		//		int carid = car.Id;
		//		var file = "";
		//		try
		//		{
		//			file = Directory.GetFiles(Path.Combine("wwwroot/img", carid.ToString())).FirstOrDefault();
		//		}
		//		catch (System.Exception){}
		//		if(file != null){
		//			var tpl = new Tuple<Car, string>(car, file);
		//			mdel.Cars.Add(tpl);
		//		}	
		//	}
		//	mdel.Departments = await _db.Departments.ToListAsync();
		//	return View("Offer", model: mdel);
		//}

		//public async Task<IActionResult> Offer(){
		//	List<Car> cars = await _db.Cars.ToListAsync();
		//	return View(cars);
		//}

		//public async Task<IActionResult> Car(int? Id){
		//	if(Id != null){
		//		SingleCarViewModel carVM = new();
		//		var car = await _db.Cars.FirstOrDefaultAsync(m=>m.Id.Equals(Id));
		//		if(car != null){
		//			carVM.Car = car;
		//			var img = Directory.GetFiles(Path.Combine("wwwroot/img/", carVM.Car.Id.ToString())).FirstOrDefault();
		//			if(img != null){
		//				carVM.Image = img;
		//			}
		//			var imgs = Directory.GetFiles(Path.Combine("wwwroot/img/", carVM.Car.Id.ToString(), "gallery")).ToList();
		//			if(imgs.Count() > 0){
		//				carVM.Images = imgs;
		//			}
		//			return View(carVM);
		//		}
		//	}
		//	return RedirectToAction(nameof(Offer));
		//}

		//[HttpPost, ActionName("Create")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> CreatePost(SingleCarViewModel model){
		//	string? userId = null;
		//	try
		//	{
		//		var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		//		if(_userId != null){
		//			userId = _userId;
		//		}
		//	}
		//	catch (System.Exception){}
		//	if(userId != null){
		//		Rent rent = new(){
		//			UserId = userId,
		//			CarId = model.Car.Id,
		//			Since = model.Since,
		//			Until = model.Until	
		//		};
		//		var numOfDays = rent.Until - rent.Since;
		//		// Idk whats next
		//		return RedirectToAction("Index", "User");
		//	}
		//	return RedirectToAction("Index", controllerName: "Login");
		//}
	}
}