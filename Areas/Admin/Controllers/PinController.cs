using Newtonsoft.Json;
using rentend.Data;
using rentend.Models.ViewModels;
using System.Text;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class PinController :Controller
{
    [BindProperty]
    public PinViewModel _pin {get;set;}
    public PinController()
    {
        _pin = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Pin> list = new();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Pins"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Pin>>(apiResponse);
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
        using (HttpClient httpClient = new())
		{
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Cars"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.Cars = JsonConvert.DeserializeObject<List<Car>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
        }
        return View(_pin);
    }

    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Cars"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.Cars = JsonConvert.DeserializeObject<List<Car>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }

            StringContent content = new(JsonConvert.SerializeObject(_pin.pin), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("https://api.rentend.koniec.dev/api/Pins", content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.pin = JsonConvert.DeserializeObject<Pin>(apiResponse);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Cars"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.Cars = JsonConvert.DeserializeObject<List<Car>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Pins/{id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.pin = JsonConvert.DeserializeObject<Pin>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(_pin);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update()
    {
        using (HttpClient httpClient = new())
        {
            StringContent content = new(JsonConvert.SerializeObject(_pin.pin), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PatchAsync($"https://api.rentend.koniec.dev/api/Pins/{_pin.pin.Id}", content))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(_pin);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Pins/{id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _pin.pin = JsonConvert.DeserializeObject<Pin>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(_pin);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.DeleteAsync($"https://api.rentend.koniec.dev/api/Pins/{_pin.pin.Id}"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(_pin);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
}