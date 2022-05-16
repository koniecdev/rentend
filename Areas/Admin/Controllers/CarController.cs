using rentend.Data;
namespace rentend.Admin.Controllers;

[Area("Admin")]
public class CarController :Controller
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Car car {get;set;}
    public CarController(ApplicationDbContext db)
    {
        _db = db;
        car = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Car> cars = await _db.Cars.ToListAsync();
        return View();
    }
    public IActionResult Create()
    {
        return View(car);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        if(ModelState.IsValid){
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View();
    }
}