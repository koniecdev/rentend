using Newtonsoft.Json;
using rentend.Data;
using rentend.Models.ViewModels;
using System.Text;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class CarController :Controller
{
    [BindProperty]
    public CarViewModel _car {get;set;}
    public CarController()
    {
        _car = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Car> list = new();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Cars"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Car>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
        }
        return View(list);
    }
    public async Task<IActionResult> Create()
    {
        _car = new();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Brands"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _car.Brands = JsonConvert.DeserializeObject<List<Brand>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Departments"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _car.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
        }
        return View(_car);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        if(_car.Car.Model != null && _car.Car.Engine != null)
		{
            using (var httpClient = new HttpClient())
            {
                StringContent content = new(JsonConvert.SerializeObject(_car.Car), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://api.rentend.koniec.dev/api/Cars", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _car.Car = JsonConvert.DeserializeObject<Car>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }

            if (_car.Car.Id != 0)
			{
                string pth = Path.Combine("wwwroot/img", _car.Car.Id.ToString());
                Directory.CreateDirectory(pth);
                var pic = Request.Form.Files.FirstOrDefault(m=>m.Name == "file");
                if(pic != null)
				{
                    string pth2 = Path.Combine(pth, pic.FileName);
                    using (FileStream fileStream = new(pth2, FileMode.Create))
					{
                        await pic.CopyToAsync(fileStream);
					}
				}
                pth = Path.Combine(pth, "gallery");
                Directory.CreateDirectory(pth);
                foreach (var pics in Request.Form.Files.Where(m => m.Name == "files"))
                {
                    string pth2 = Path.Combine(pth, pics.FileName);
                    using (FileStream fileStream = new(pth2, FileMode.Create))
                    {
                        await pics.CopyToAsync(fileStream);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
		}
        TempData["error"] = "Please ensure provided data are valid.";
        return View(_car);
    }
    public async Task<IActionResult> Update(int Id)
    {
        if(Id > 0)
		{
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Brands"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _car.Brands = JsonConvert.DeserializeObject<List<Brand>>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
                using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Departments"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _car.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
                using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Cars/{Id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _car.Car = JsonConvert.DeserializeObject<Car>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (_car.Car != null)
			{
		        return View(_car);
			}
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePost()
    {
        using (HttpClient httpClient = new())
        {
            StringContent content = new(JsonConvert.SerializeObject(_car.Car), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PatchAsync($"https://api.rentend.koniec.dev/api/Cars/{_car.Car.Id}", content))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(_car);
                }
            }
        }
        string pth = Path.Combine("wwwroot/img", _car.Car.Id.ToString());
        if (!Directory.Exists(pth))
        {
            Directory.CreateDirectory(pth);
        }
        var pic = Request.Form.Files.FirstOrDefault(m => m.Name == "file");
        if (pic != null)
        {
            var existinFiles = Directory.GetFiles(pth);
            if (existinFiles.Count() > 0)
            {
                foreach (var existin in existinFiles)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(existin);
                }
            }
            string pth2 = Path.Combine(pth, pic.FileName);
            using (FileStream fileStream = new(pth2, FileMode.Create))
            {
                await pic.CopyToAsync(fileStream);
            }
        }
        pth = Path.Combine(pth, "gallery");
		if (!Directory.Exists(pth))
		{
            Directory.CreateDirectory(pth);
		}
        foreach (var pics in Request.Form.Files.Where(m => m.Name == "files"))
        {
            string pth2 = Path.Combine(pth, pics.FileName);
            using (FileStream fileStream = new(pth2, FileMode.Create))
            {
                await pics.CopyToAsync(fileStream);
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? Id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Cars/{Id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _car.Car = JsonConvert.DeserializeObject<Car>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(_car);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost()
	{
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.DeleteAsync($"https://api.rentend.koniec.dev/api/Cars/{_car.Car.Id}"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(_car);
                }
            }
        }
        string pth = Path.Combine("wwwroot/img", _car.Car.Id.ToString());
        if (Directory.Exists(pth))
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Directory.Delete(pth, true);
        }
        return RedirectToAction(nameof(Index));
    }
}