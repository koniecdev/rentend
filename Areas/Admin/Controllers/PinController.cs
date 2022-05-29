using rentend.Data;
using rentend.Models.ViewModels;

namespace rentend.Admin.Controllers;

[Area("Admin")]
public class PinController :Controller
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public PinViewModel _pin {get;set;}
    public PinController(ApplicationDbContext db)
    {
        _db = db;
        _pin = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Pin> pins = await _db.Pins.Include(m=>m.Car).ToListAsync();
        return View(pins);
    }
    public IActionResult Create()
    {
        _pin = new()
        {
            Cars = _db.Cars.ToList(),
            Pin = new()
        };
        return View(_pin);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        _db.Pins.Add(_pin.Pin);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? Id)
    {
        if(Id != null)
		{
            _pin.Cars = await _db.Cars.ToListAsync();
		    _pin.Pin = await _db.Pins.FirstOrDefaultAsync(m=>m.Id.Equals(Id));
            if(_pin.Pin != null)
			{
		        return View(_pin);
			}
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePost()
    {
        var fromDb = await _db.Pins.FirstOrDefaultAsync(m=>m.Id == _pin.Pin.Id);
        fromDb.CarId = _pin.Pin.CarId;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? Id)
    {
        if (Id != null)
        {
            _pin.Pin = await _db.Pins.FirstOrDefaultAsync(m => m.Id.Equals(Id));
            if (_pin.Pin != null)
            {
                return View(_pin);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost()
	{
        _db.Pins.Remove(_pin.Pin);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
	}
}