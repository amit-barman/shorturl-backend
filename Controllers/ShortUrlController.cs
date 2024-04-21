using Microsoft.AspNetCore.Mvc;
using shorturl.Model;
using shorturl.Utils;
using System.Linq;
using shorturl.Authentication;

namespace shorturl.Controllers;

[ApiController]
[Route("/")]
public class ShortUrlController : ControllerBase
{
    private static readonly int _shortUrlLen = 6;

    [HttpPost("short")]
    [ServiceFilter(typeof(AuthFilter))]
    public async Task<ActionResult> shortURL( UserInput longurl ){

        try {
            using var db = new ShortUrlDBContext();
        
            if(!Uri.TryCreate(longurl.longUrl, UriKind.Absolute, out _))
            {
                return BadRequest("Invalid Url");
            } else {
                string shorturl = RandomUrl.getRandomURL(_shortUrlLen);

                while(db.Urls.Any(x => x.shortUrl == shorturl)){
                    shorturl = RandomUrl.getRandomURL(_shortUrlLen);
                }

                URL url = new URL
                {
                    Id = Guid.NewGuid().ToString(),
                    realUrl = longurl.longUrl,
                    shortUrl = $"{Request.Scheme}://{Request.Host.Value}/{shorturl}",
                    creationTime = DateTime.Now.ToString()
                };
                db.Add(url);
                db.SaveChanges();

                return Ok(url);
            }
        }
        catch(Exception e){
            return BadRequest("Something Went Wrong!");
        }
    }

    [HttpGet("/{shorturlEndpoint}")]
    public async Task<ActionResult> retrieveRealUrl( string shorturlEndpoint ){
        try {
            using var db = new ShortUrlDBContext();
            URL resp = db.Urls.Single(b => b.shortUrl.Contains(shorturlEndpoint));
            return Redirect(resp.realUrl);
        } catch(Exception e){
            Console.WriteLine(e);
            return BadRequest("Requested Endpoint Not Found!");
        }
    }
}
