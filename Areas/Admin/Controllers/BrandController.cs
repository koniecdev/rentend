using Newtonsoft.Json;
using rentend.Data;
using System.Text;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class BrandController :Controller
{
    [BindProperty]
    public Brand brand {get;set;}
    public BrandController()
    {
        brand = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Brand> list = new();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Brands"))
            {
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
				{
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Brand>>(apiResponse);
				}
				else
				{
                    ViewBag.StatusCode = response.StatusCode;
				}
            }
        }
        return View(list);
    }

    public IActionResult Create()
	{
        return View(brand);
	}

    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
	{
        using(HttpClient httpClient = new())
		{
            StringContent content = new(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("https://api.rentend.koniec.dev/api/Brands", content))
			{
                if(response.StatusCode == System.Net.HttpStatusCode.Created)
				{
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    brand = JsonConvert.DeserializeObject<Brand>(apiResponse);
				}
			}
		}
        return RedirectToAction(nameof(Index));
	}
    public async Task<IActionResult> Update(int id)
	{
        using(HttpClient httpClient = new())
		{
            using(var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Brands/{id}"))
			{
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
				{
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    brand = JsonConvert.DeserializeObject<Brand>(apiResponse);
				}
				else
				{
                    return RedirectToAction(nameof(Index));
				}
			}
		}
        return View(brand);
	}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update()
	{
        using (HttpClient httpClient = new())
        {
            StringContent content = new(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PatchAsync($"https://api.rentend.koniec.dev/api/Brands/{brand.Id}", content))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(brand);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Brands/{id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    brand = JsonConvert.DeserializeObject<Brand>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(brand);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
	{
        using (HttpClient httpClient = new())
		{
            using (var response = await httpClient.DeleteAsync($"https://api.rentend.koniec.dev/api/Brands/{brand.Id}"))
			{
                if(response.StatusCode != System.Net.HttpStatusCode.NoContent)
				{
                    return View(brand);
				}
			}
		}
        return RedirectToAction(nameof(Index));
	}
}