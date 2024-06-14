using shorturl.Dto;
using shorturl.Model;
using shorturl.Data;
using shorturl.Utils;

namespace shorturl.Repository;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly ShortUrlDBContext _context;
    private readonly IHttpContextAccessor _httpcontext;
    private static readonly int _shortUrlLen = 6;

    public ShortUrlRepository( ShortUrlDBContext context, IHttpContextAccessor httpcontext )
    {
        _context = context;
        _httpcontext = httpcontext;
    }

    // Create Short URL from Real/Long url
    public URL? createsShortURL( UserInput longurl )
    {
        try
        {
            if(!Uri.TryCreate(longurl.longUrl, UriKind.Absolute, out _))
            {
                return null;
            }
            else 
            {
                string shorturl = RandomUrl.getRandomURL(_shortUrlLen);

                while(_context.Urls.Any(x => x.shortUrl == shorturl))
                {
                    shorturl = RandomUrl.getRandomURL(_shortUrlLen);
                }

                URL url = new URL
                {
                    Id = Guid.NewGuid().ToString(),
                    realUrl = longurl.longUrl,
                    shortUrl = $"{_httpcontext.HttpContext.Request.Scheme}://{_httpcontext.HttpContext.Request.Host.Value}/{shorturl}",
                    creationTime = DateTime.Now.ToString()
                };
                _context.Add(url);
                _context.SaveChanges();

                return url;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    // Extract the short URL if exists in the database
    public string? extractShortURL( string shorturlEndpoint )
    {
        try
        {
            URL resp = _context.Urls.Single(b => b.shortUrl.Contains(shorturlEndpoint));
            if(resp == null)
            {
                return null;
            }
            return (resp.realUrl);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}