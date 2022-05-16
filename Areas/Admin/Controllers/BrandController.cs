using rentend.Data;
namespace rentend.Admin.Controllers;

[Area("Admin")]
public class BrandController :Controller
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Brand brand {get;set;}
    public BrandController(ApplicationDbContext db)
    {
        _db = db;
        brand = new();
    }
    public async Task<IActionResult> Index()
    {
        List<Brand> brands = await _db.Brands.ToListAsync();
        return View(brands);
    }
    public IActionResult Create()
    {
        return View(brand);
    }
    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost()
    {
        if(ModelState.IsValid){
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(brand);
    }
    public async Task<IActionResult> Update(int? id)
    {
        if(id != null){
            var x = await _db.Brands.FirstOrDefaultAsync(m=>m.Id.Equals(id));
            if(x != null){
                brand = x;
                return View(brand);
            }
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePost()
    {
        if(ModelState.IsValid){
            var fromDb = await _db.Brands.FirstOrDefaultAsync(m=>m.Id.Equals(brand.Id));
            if(fromDb != null){
                fromDb.Name = brand.Name;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(brand);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if(id != null){
            var x = await _db.Brands.FirstOrDefaultAsync(m=>m.Id.Equals(id));
            if(x != null){
                brand = x;
                return View(brand);
            }
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost()
    {
        var fromDb = await _db.Brands.FirstOrDefaultAsync(m=>m.Id.Equals(brand.Id));
        if(fromDb != null){
            _db.Remove(fromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Please ensure provided data are valid.";
        return View(brand);
    }
}