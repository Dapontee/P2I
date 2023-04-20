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

    public IActionResult SeConnecter()
    {
        if (HttpContext.Session.GetString("Name") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index");
        }
    }


    [HttpPost]
    public ActionResult SeConnecter(Administrateur u)
    {
        if (HttpContext.Session.GetString("Pseudo") == null)
        {
            using (MvcJournalContext db = new MvcJournalContext())
            {
                var user = db.Administrateurs.Where(a => a.Pseudo.Equals(u.Pseudo)).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("Pseudo", "Le nom d'utilisateur n'existe pas");
                    return View("SeConnecter", user);
                }
                var obj = db.Administrateurs.Where(a => a.Pseudo.Equals(u.Pseudo) && a.Mdp.Equals(u.Mdp)).FirstOrDefault();
                if (obj != null)
                {
                    HttpContext.Session.SetString("Pseudo", obj.Pseudo.ToString());
                    HttpContext.Session.SetString("Id", obj.Id.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Mdp", "Le mot de passe est incorrect");
                    return View("SeConnecter", user);
                }
            }

        }

        return RedirectToAction("SeConnecter");


    }

    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("Pseudo");
        return RedirectToAction("SeConnecter");
    }

    // GET: Livre
    public async Task<IActionResult> Index()
    {
        var numeros = await _context.Journeaux.OrderBy(m => m.Titre).ToListAsync();
        return View(numeros);


    }


    public async Task<IActionResult> PDF(int id)
    {
        var journal = await _context.Journeaux.Where(x => x.Numero == id).FirstOrDefaultAsync();

        return View(journal);
    }



    public IActionResult Create()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateForm([Bind("Id, Titre, Genre, Couverture, Numero, DateParution")] Journal journal)
    {
        // Apply model validation rules
        if (ModelState.IsValid)
        {
            _context.Add(journal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // At this point, something failed: redisplay form
        return View(journal);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var journal = await _context.Journeaux
            .FirstOrDefaultAsync(m => m.Id == id);
        if (journal == null)
        {
            return NotFound();
        }

        return View(journal);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var journal = await _context.Journeaux.FindAsync(id);
        _context.Journeaux.Remove(journal!);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}