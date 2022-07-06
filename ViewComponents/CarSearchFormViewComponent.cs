using Newtonsoft.Json;
using rentend.Models.ViewModels;
using rentend.Utility;

namespace rentend.ViewComponents;
public class CarSearchFormViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        IndexViewModel formModel = new();
        using(var httpClient = new HttpClient()){
            using (var response = await httpClient.GetAsync("https://api.rentend.koniec.dev/api/Departments"))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    formModel.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }
        }
        if(HttpContext.Session.GetInt32(SD.departmentId) != null && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SD.rentSince))
         && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SD.rentTo)))
        {
            formModel.DepartmentId = Convert.ToInt32(HttpContext.Session.GetInt32(SD.departmentId));
            formModel.RentSince = DateTime.Parse(HttpContext.Session.GetString(SD.rentSince));
            formModel.RentTo = DateTime.Parse(HttpContext.Session.GetString(SD.rentTo));
        }
        return View(formModel);
    }
}