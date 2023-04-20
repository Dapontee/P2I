
public class Journal
{
    public int Id { get; set; }
    public string? Titre { get; set; }
    public string Genre { get; set; } = null!;
    public string Couverture { get; set; } = null!;
    public int Numero { get; set; }
    public DateTime DateParution { get; set; }

}