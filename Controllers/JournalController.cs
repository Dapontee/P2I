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

// Post qui connecte l'administrateur si les identifiants rentrés sont corrects
//Crée une session s'il est connecté car il n'y a qu'un seul utilisateur dans l'appli qui est aussi administrateur
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
//Action qui déconnecte la session de l'administrateur
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("Pseudo");
        return RedirectToAction("SeConnecter");
    }

    // GET: Journal, acces à l'index des jouneaux, permet de les afficher par année
    public IActionResult Index()
    {
        var groupeJourneaux = _context.Journeaux.GroupBy(j=>j.DateParution.Year);
        return View(groupeJourneaux);

    }

//Vue de l'edition d'un journal (modifier)
    public async Task<IActionResult>  Edit(int? id)
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

// Enregistrer la modif d'un journal
 [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enregistrer (int id, [Bind("Id, Titre, Couverture, DateParution, Numero")] Journal journaldto)
    {
        {
            if (id != journaldto.Id)
            {
                return NotFound();
            }
            var journal = _context.Journeaux.Find(id);

            if (ModelState.IsValid)
            {
                try
                {
                    journal!.Update(journaldto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExists(journaldto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(journaldto);
        }
    }
       private bool JournalExists(int id)
    {
        return _context.Journeaux.Any(e => e.Id == id);
    }

    //Afficher le pdf en le convertissant en bit puis utilisant PDF.js
    public IActionResult PDF(int id)
    {
      
    string ReportURL = string.Format("{0}.pdf",id);  
    byte[] FileBytes =  System.IO.File.ReadAllBytes("wwwroot/PDF/"+ ReportURL);  
    return File(FileBytes, "application/PDF"); 
       
    }

//Vue qui permet de créer un journal
    public IActionResult Create()
    {
        return View();
    }

//Form qui valide la création d'un journal dans la bd
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

    // Supprimer un journal de la bd
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

//Post qui supprime le journal de la bd

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