// Controller important qui permet de créer, éditer et supprimer un article dans les onglets du site

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


public class ArticleController : Controller
{
    private readonly MvcJournalContext _context;

    public ArticleController(MvcJournalContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)
    {
      
        if (id == null)
        {
            return NotFound();
        }
        var article = await _context.Articles
            .FirstOrDefaultAsync(m => m.Id == id);

        if (article == null)
        {
            return NotFound();
        }

        return View(article);


    }

    public IActionResult Create()
    {
        
        return View();
    }

public async Task<IActionResult>  Edit(int? id)
    {
       if (id == null)
        {
            return NotFound();
        }

        var article = await _context.Articles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);


    }

    //Post pour enregistrer un article dans la bd
 [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enregistrer (int id, [Bind("Id, Titre, Texte, Illustration, CategorieId")] Article articledto)
    {

         //jointure
        var categorie = _context.Categories.Find(articledto.CategorieId);

      
        articledto.CategorieArticle = categorie!;

        
            if (id != articledto.Id)
            {
                return NotFound();
            }
            var article = _context.Articles.Find(id);

        
               
                    article!.Update(articledto);
                    await _context.SaveChangesAsync();
             
                return RedirectToAction(nameof(Create));
            
           
        }
    
       private bool ArticleExists(int id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }

//Controller qui gere la vue de la suppression d'un article
     public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = await _context.Articles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }

// Post pour supprimer un article de la bd

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        _context.Articles.Remove(article!);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Create));
    }

//Form pour créer article 

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateForm([Bind("Id,Texte,Titre, Illustration, CategorieId")] Article article)
    {
        //jointure
        var categorie = _context.Categories.Find(article.CategorieId);

       

        article.CategorieArticle = categorie!;

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

 
        return RedirectToAction(nameof(Create));

    }


}