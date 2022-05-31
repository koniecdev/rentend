using rentend.Data;
namespace rentend.Admin.Controllers;

[Area("Admin")]
public class DepartamentController :Controller
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Departament departament {get;set;}
    public DepartamentController(ApplicationDbContext db)
    {
        _db = db;
        departament = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Departament> departaments = await _db.Departaments.ToListAsync();
        return View(departaments);
    }
    public IActionResult Create()
    {
        return View(departament);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        if(ModelState.IsValid){
            _db.Departaments.Add(departament);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(departament);
    }
    public async Task<IActionResult> Update(int? id)
    {
        if(id != null){
            var x = await _db.Departaments.FirstOrDefaultAsync(m=>m.Id.Equals(id));
            if(x != null){
                departament = x;
                return View(departament);
            }
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePost()
    {
        if(ModelState.IsValid){
            var fromDb = await _db.Departaments.FirstOrDefaultAsync(m=>m.Id.Equals(departament.Id));
            if(fromDb != null){
                fromDb.City = departament.City;
                fromDb.FullAddress = departament.FullAddress;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(departament);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if(id != null){
            var x = await _db.Departaments.FirstOrDefaultAsync(m=>m.Id.Equals(id));
            if(x != null){
                departament = x;
                return View(departament);
            }
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost()
    {
        var fromDb = await _db.Departaments.FirstOrDefaultAsync(m=>m.Id.Equals(departament.Id));
        if(fromDb != null){
            _db.Remove(fromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(departament);
    }
}