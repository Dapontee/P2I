public class Article
{

    public int Id { get; set; }
    public string Texte { get; set; } = null!;
    public string Titre { get; set; } = null!;
    public Categorie CategorieArticle { get; set; } = null!;
    public int JournalId { get; set; }
}