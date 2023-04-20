//Controller permet de créer une catégorie 

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


public class CategorieController : Controller
{
    private readonly MvcJournalContext _context;

    public CategorieController(MvcJournalContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Create()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateForm([Bind("Id, Nom")] Categorie categorie)
    {

        // Apply model validation rules
        if (ModelState.IsValid)
        {
            _context.Add(categorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
        // At this point, something failed: redisplay form
        return View(categorie);

    }


}