// Seedata de test pour tester la bd à ses débuts, plus vraiment utile mais si jamais elle peut servir, elle est ici
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
               
                DateParution = DateTime.Parse("1932-10-15"),
                Couverture = "https://imageshack.com/i/pmtQ1HBEp",
                Numero = 2,
            };

            context.Journeaux.AddRange(


                 VoyageAuBoutDeLaNuit

            );
            Administrateur Proust = new Administrateur
            {
                Pseudo = "MP",
                Mdp = "123",

            };
            context.Administrateurs.AddRange(
                Proust
            );

            Categorie BDE = new Categorie
            {
                Nom = "BDE",

            };
            context.Categories.AddRange(
               BDE
           );
            Article Promo2024 = new Article
            {
                Titre = "Promo2024",
                Texte = "et voici la prendjzduzdefcjnnddsdhbsdhsxhshbhbshbschbcshbcshbschbhbcs",
                CategorieId = 1,


            };
            context.Articles.AddRange(
               Promo2024
           );

            context.SaveChanges();
        }
    }
}