using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rentend.Data;
using rentend.Models;
using rentend.Models.ViewModels;
using rentend.Utility;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace rentend.Home.Controllers
{
	[Area("Home")]
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index(HomeViewModel model)
		{
			List<Pin>? pins = new();
			foreach (var pin in pins){
				int carid = pin.CarId;
				try
				{
					var file = Directory.GetFiles(Path.Combine("wwwroot/img", carid.ToString())).FirstOrDefault();
					if(file != null){
						var tpl = new Tuple<Car, string>(pin.Car, file);
						model.Cars.Add(tpl);
					}
				}
				catch(Exception)
				{

				}
			}
			UpdateSessionValues(model.IndexVM);
			return View(model);
		}

		public async Task<IActionResult> Cars(IndexViewModel model)
		{
			List<Car>? rentedCars = new();
			List<Car>? carsFromGivenDepartment = new();
			using(var httpClient = new HttpClient())
			{
				using (var response = await httpClient
					.GetAsync($"https://api.rentend.koniec.dev/api/Rent/GetByDate/{model.DepartmentId}?since={model.RentSince.ToString("yyyy-MM-ddTHH:mm:ss")}&until={model.RentTo.ToString("yyyy-MM-ddTHH:mm:ss")}"))
				{
					if(response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var apiResult = await response.Content.ReadAsStringAsync();
						var x = JsonConvert.DeserializeObject<List<Rent>>(apiResult);
						rentedCars = x.Select(m => m.Car).ToList();
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
				using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Cars/GetByDepartmentId/{model.DepartmentId}"))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var content = await response.Content.ReadAsStringAsync();
						carsFromGivenDepartment = JsonConvert.DeserializeObject<List<Car>>(content);
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			List<Tuple<Car, string>> automobiles = new();
			var availableCars = carsFromGivenDepartment.Where(m => !rentedCars.Any(m1 => m1.Id == m.Id)).ToList();
			foreach (var car in availableCars)
			{
				try
				{
					var pic = Directory.GetFiles($"wwwroot/img/{car.Id}/").FirstOrDefault();
					automobiles.Add(new Tuple<Car, string>(car, pic));
				}
				catch (Exception)
				{
					automobiles.Add(new Tuple<Car, string>(car, ""));
				}
			}
			UpdateSessionValues(model);
			return View("Cars", automobiles);
		}

		public async Task<IActionResult> Offer(HomeViewModel model)
		{
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Cars"))
				{
					if(response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						var Cars = JsonConvert.DeserializeObject<List<Car>>(apiResponse);
						if(Cars != null)
						{
							foreach(var car in Cars)
							{
								try
								{
									string? file = Directory.GetFiles($"wwwroot/img/{car.Id}").FirstOrDefault();
									if (file != null)
									{
										model.Cars.Add(new Tuple<Car, string>(car, file));
									}
								}
								catch(Exception)
								{

								}
							}
						}
					}
					else
					{
						ViewBag.StatusCode = response.StatusCode;
					}
				}
			}
			UpdateSessionValues(model.IndexVM);
			return View("Cars", model);
		}

		public async Task<IActionResult> Car(int Id)
		{
			SingleCarViewModel model = new();
			if(HttpContext.Session.GetInt32("departmentId") != null)
			{
				model.IndexVM = new()
				{
					DepartmentId = Convert.ToInt32(HttpContext.Session.GetInt32("departmentId")),
					RentSince = DateTime.Parse(HttpContext.Session.GetString("departmentId")),
					RentTo = DateTime.Parse(HttpContext.Session.GetString("departmentId"))
				};
			}
			using(var httpClient = new HttpClient())
			{
				using(var apiResponse = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Cars/{Id}"))
				{
					if(apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var stringResult = await apiResponse.Content.ReadAsStringAsync();
						model.Car = JsonConvert.DeserializeObject<Car>(stringResult);
					}
					else
					{
						return View(nameof(Index));
					}
				}
			}
			try
			{
				model.Image = Directory.GetFiles($"wwwroot/img/{Id}").First();
				foreach(var file in Directory.GetFiles($"wwwroot/img/{Id}/gallery"))
				{
					model.Images.Add(file);
				}
			}
			catch (Exception)
			{

			}
			return View(model);
		}
		public void UpdateSessionValues(IndexViewModel IndexVM)
		{
			HttpContext.Session.SetInt32(SD.departmentId, IndexVM.DepartmentId);
			HttpContext.Session.SetString(SD.rentSince, IndexVM.RentSince.ToString());
			HttpContext.Session.SetString(SD.rentTo, IndexVM.RentTo.ToString());
		}

	}
}