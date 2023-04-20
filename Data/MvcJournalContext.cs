using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


public class MvcJournalContext : DbContext
{
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Administrateur> Administrateurs { get; set; } = null!;
    public DbSet<Categorie> Categories { get; set; } = null!;
    public DbSet<Journal> Journeaux { get; set; } = null!;



    public string DbPath { get; private set; }

    public MvcJournalContext()
    {
        // Path to SQLite database file
        DbPath = "MvcJournal.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        // Optional: log SQL queries to console
        options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}

