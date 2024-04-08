using Microsoft.EntityFrameworkCore;

namespace shorturl.Model;

public class ShortUrlDBContext : DbContext
{
	public DbSet<URL> Urls { get; set; }

	public string DbPath { get; }

    public ShortUrlDBContext()
    {
        DbPath = "./database/shorturl.db";
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}