using rentend.Data;
using rentend.Models.ViewModels;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class CarController :Controller
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public CarViewModel _car {get;set;}
    public CarController(ApplicationDbContext db)
    {
        _db = db;
        _car = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Car> cars = await _db.Cars.Include(m=>m.Brand).ToListAsync();
        return View(cars);
    }
    public IActionResult Create()
    {
        _car = new()
        {
            Brands = _db.Brands.ToList(),
            Car = new()
        };
        return View(_car);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        if(_car.Car.Model != null && _car.Car.Engine != null)
		{
            _db.Cars.Add(_car.Car);
            await _db.SaveChangesAsync();
            if(_car.Car.Id != 0)
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
    public async Task<IActionResult> Update(int? Id)
    {
        if(Id != null)
		{
            _car.Brands = await _db.Brands.ToListAsync();
		    _car.Car = await _db.Cars.FirstOrDefaultAsync(m=>m.Id.Equals(Id));
            if(_car.Car != null)
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
        if (_car.Car.Model != null && _car.Car.Engine != null)
        {
            var fromDb = await _db.Cars.FirstOrDefaultAsync(m => m.Id.Equals(_car.Car.Id));
            if (fromDb != null)
			{
                fromDb.PricePerDayWeekend = _car.Car.PricePerDayWeekend;
                fromDb.PricePerDay = _car.Car.PricePerDay;
                fromDb.PricePerWeekend = _car.Car.PricePerWeekend;
                fromDb.PricePerWeek = _car.Car.PricePerWeek;
                fromDb.PricePerDayWeekend= _car.Car.PricePerDayWeekend;
                fromDb.PricePerMonth = _car.Car.PricePerMonth;
                fromDb.BrandId = _car.Car.BrandId;
                fromDb.Drive = _car.Car.Drive;
                fromDb.Engine = _car.Car.Engine;
                fromDb.Model = _car.Car.Model;
                fromDb.Horsepower = _car.Car.Horsepower;
                fromDb.Transmission = _car.Car.Transmission;
                fromDb.YearOfProduction = _car.Car.YearOfProduction;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
			}
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(_car);
    }
    public async Task<IActionResult> Delete(int? Id)
    {
        if (Id != null)
        {
            _car.Car = await _db.Cars.FirstOrDefaultAsync(m => m.Id.Equals(Id));
            if (_car.Car != null)
            {
                return View(_car);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost()
	{
        _db.Cars.Remove(_car.Car);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
	}
}