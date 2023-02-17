public class Categorie
{

    public int id { get; set; }

    public string Nom { get; set; } = null!;
    public int ArticleId { get; set; }

    public List<Article> ListeDesArticles = new List<Article>();

}