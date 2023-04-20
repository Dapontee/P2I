
public class Journal
{
    public int Id { get; set; }
    public string? Titre { get; set; }
    public string Couverture { get; set; } = null!;
    public int? Numero { get; set; }
    public DateTime DateParution { get; set; }

// Pour gérer les dto dans la modification d'un journal
     public void Update(Journal journaldto)
    {
        if (journaldto.Titre != null && journaldto.Titre != Titre)
            {Titre = journaldto.Titre;}
         if (journaldto.DateParution != null && journaldto.DateParution != DateParution)
            {DateParution = journaldto.DateParution;}
         if (journaldto.Couverture != null && journaldto.Couverture != Couverture)
            {Couverture = journaldto.Couverture;}
         if (journaldto.Numero != null && journaldto.Numero != Numero)
            {Numero = journaldto.Numero;}

    }

}