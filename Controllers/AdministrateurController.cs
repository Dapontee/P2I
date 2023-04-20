using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;





public class AdministrateurController : Controller
{
    private readonly MvcJournalContext _context;

    public AdministrateurController(MvcJournalContext context)
    {
        _context = context;
    }

    // GET: Livre


    //public IActionResult SeConnecter()
    //{
    //    return View();
    //}

    public async Task<IActionResult> Index()
    {
        var numeros = await _context.Journeaux.OrderBy(m => m.Titre).ToListAsync();
        return View(numeros);
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
    /*
        public async Task<IActionResult> LivresAlire(string? pseudo, string? mdp, int? id)
        {
            if(pseudo != null && mdp !=null)
            {
                var utilisateur = await _context.Administrateurs.Where(m=>m.Pseudo==pseudo).Where(m=>m.Mdp==mdp).SingleOrDefaultAsync();
                if (utilisateur == null)
                {
                    return NotFound();
                }
                var livresALire = await _context.Administrateurs.Where(m=>m.Id==utilisateur.Id).OrderBy(m=>m.).Include(m=>m.Livre).ToListAsync();
                return View(livresALire); 
            }
            else 
            {
                var utilisateur = await _context.Utilisateurs.FindAsync(id);
                if (utilisateur == null)
                {
                    return NotFound();
                }
                var livresALire = await _context.LivresALire.Where(m=>m.UtilisateurId==utilisateur.Id).OrderBy(m=>m.CategorieLivre).Include(m=>m.Livre).ToListAsync();
                return View(livresALire); 
            }

        }*/

}

/*
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Inscription([Bind("Pseudo, Mdp, GenrePrefere, AuteurPrefere")] Utilisateur utilisateur)
    {
        if (ModelState.IsValid)
        {
            _context.Add(utilisateur);
            var bibliotheque = new Bibliotheque{
                    UtilisateurId = utilisateur.Id,
                    Utilisateur = utilisateur,
                    ListeLivresALire = new List<LivreALire> {},
                    ListeLivresLus = new List<LivreLu> {}};
            _context.Bibliotheques.Add(bibliotheque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // At this point, something failed: redisplay form
        return View();
    }

    public async Task<IActionResult> LivresAlire(string? pseudo, string? mdp, int? id)
    {
        if(pseudo != null && mdp !=null)
        {
            var utilisateur = await _context.Utilisateurs.Where(m=>m.Pseudo==pseudo).Where(m=>m.Mdp==mdp).SingleOrDefaultAsync();
            if (utilisateur == null)
            {
                return NotFound();
            }
            var livresALire = await _context.LivresALire.Where(m=>m.UtilisateurId==utilisateur.Id).OrderBy(m=>m.CategorieLivre).Include(m=>m.Livre).ToListAsync();
            return View(livresALire); 
        }
        else 
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            var livresALire = await _context.LivresALire.Where(m=>m.UtilisateurId==utilisateur.Id).OrderBy(m=>m.CategorieLivre).Include(m=>m.Livre).ToListAsync();
            return View(livresALire); 
        }

    }

    public async Task<IActionResult> LivresLus(int? id)
    {
        if (id == null)
        {
            return NotFound();
        } 
        
        var livresLus = await _context.LivresLus.Where(m=>m.UtilisateurId==id).OrderBy(m=>m.Livre.Livre.Titre).Include(m=>m.Livre.Livre).ToListAsync();
        return View(livresLus);
    } 
    
    // GET: Livre/Details/5
    public async Task<IActionResult> DetailsLivreAlire(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livre = await _context.LivresALire.Where(m=>m.Id==id).Include(m=>m.Livre).FirstOrDefaultAsync();
        if (livre == null)
        {
            return NotFound();
        }
        return View(livre);
    }

    public async Task<IActionResult> Lu(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livreALire = await _context.LivresALire.FindAsync(id);
            
        if (livreALire == null)
        {
            return NotFound();
        }

        var livreLu = new LivreLu{
                UtilisateurId = livreALire.UtilisateurId,
                Utilisateur = livreALire.Utilisateur,
                Livre = livreALire,
                LivreId = livreALire.Id};
            
        await _context.LivresLus.AddRangeAsync(livreLu);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Connexion));
    }

    public async Task<IActionResult> Possede(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livreALire = await _context.LivresALire.FindAsync(id);
            
        if (livreALire == null)
        {
            return NotFound();
        }
        livreALire.CategorieLivre=0;
        _context.LivresALire.Update(livreALire);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Connexion));
    }

    // GET: Livre/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livre = await _context.Livres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (livre == null)
        {
            return NotFound();
        }
        

        return View(livre);
    }

    public async Task<IActionResult> DetailsLivreLu(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livre = await _context.LivresLus.Where(m=>m.Id==id).Include(m=>m.Livre.Livre).FirstOrDefaultAsync();
        if (livre == null)
        {
            return NotFound();
        }
        return View(livre);
    }

    

    public async Task<IActionResult> Add(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var livresUtilisateur = await _context.LivresALire.Where(m=>m.UtilisateurId==id).ToListAsync();
        var livres = await _context.Livres.ToListAsync();

        return RedirectToAction(nameof(Connexion));
    }

}
*/


