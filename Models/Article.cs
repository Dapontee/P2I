public class Article
{
    public int Id { get; set; }
    public string Texte { get; set; } = null!;
    public string Titre { get; set; } = null!;
    public string Illustration {get;set;}=null!;
    public Categorie CategorieArticle { get; set; } = null!;
    public int? CategorieId { get; set; } 


// Pour gérer les dto dans la modification d'un article
      public void Update(Article articledto)
    {
        if (articledto.Titre != null && articledto.Titre != Titre)
            {Titre = articledto.Titre;}
         if (articledto.Texte != null && articledto.Texte != Texte)
            {Texte = articledto.Texte;}
         if (articledto.Illustration != null && articledto.Illustration != Illustration)
            {Illustration = articledto.Illustration;}
         if (articledto.CategorieId != null && articledto.CategorieId != CategorieId)
            {CategorieId = articledto.CategorieId;}

    }

}