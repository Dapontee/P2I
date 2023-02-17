// Add students
public class SeedDataContent
{
    public static void InitDB()
    {
        using (var context = new MvcJournalContext())
        {
            // Look for existing content
            if (context.Journeaux.Any())
            {
                return;   // DB already filled
            }

            Journal VoyageAuBoutDeLaNuit = new Journal
            {
                Titre = "Voyage au bout de la nuit",
                Genre = "Roman",
                DateParution = DateTime.Parse("1932-10-15"),

            };

            context.Journeaux.AddRange(


                 VoyageAuBoutDeLaNuit

            );
            Administrateur Proust = new Administrateur
            {
                Pseudo = "MP",
                Mdp = "larecherchedeswann7",

            };
            context.Administrateurs.AddRange(
                Proust
            );

            context.SaveChanges();
        }
    }
}