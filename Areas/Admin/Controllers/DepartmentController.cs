using Newtonsoft.Json;
using rentend.Data;
using System.Text;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class DepartmentController :Controller
{
    [BindProperty]
    public Department department {get;set;}
    public DepartmentController()
    {
        department = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Department> reservationList = new();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Departments"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            }
        }
        return View(reservationList);
    }

    public IActionResult Create()
    {
        return View(department);
    }

    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        using (HttpClient httpClient = new())
        {
            StringContent content = new(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("https://api.rentend.koniec.dev/api/Departments", content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    department = JsonConvert.DeserializeObject<Department>(apiResponse);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Departments/{id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    department = JsonConvert.DeserializeObject<Department>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(department);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update()
    {
        using (HttpClient httpClient = new())
        {
            StringContent content = new(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PatchAsync($"https://api.rentend.koniec.dev/api/Departments/{department.Id}", content))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(department);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.GetAsync($"https://api.rentend.koniec.dev/api/Departments/{id}"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    department = JsonConvert.DeserializeObject<Department>(apiResponse);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        return View(department);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
    {
        using (HttpClient httpClient = new())
        {
            using (var response = await httpClient.DeleteAsync($"https://api.rentend.koniec.dev/api/Departments/{department.Id}"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return View(department);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }
}