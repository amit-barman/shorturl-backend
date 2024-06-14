using Microsoft.EntityFrameworkCore;
using shorturl.Model;

namespace shorturl.Data;

public class ShortUrlDBContext : DbContext
{
	public DbSet<URL> Urls { get; set; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    public ShortUrlDBContext(DbContextOptions<ShortUrlDBContext> options) : base(options)
    {
    	
    }
}