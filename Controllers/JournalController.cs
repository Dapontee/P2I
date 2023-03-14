using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;



public class JournalController : Controller
{
    private readonly MvcJournalContext _context;

    public JournalController(MvcJournalContext context)
    {
        _context = context;
    }

    // GET: Livre
    public async Task<IActionResult> Index()
    {
        var numeros = await _context.Journeaux.OrderBy(m => m.Titre).ToListAsync();
        return View(numeros);
    }

}