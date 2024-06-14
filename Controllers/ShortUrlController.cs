using Microsoft.AspNetCore.Mvc;
using shorturl.Repository;
using shorturl.Dto;
using shorturl.Model;
using shorturl.Utils;
using System.Linq;
using shorturl.Authentication;

namespace shorturl.Controllers;

[ApiController]
[Route("/")]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlRepository _shorturlrepository;

    public ShortUrlController( IShortUrlRepository shorturlrepository )
    {
        _shorturlrepository = shorturlrepository;
    }

    [HttpPost("short")]
    [ServiceFilter(typeof(AuthFilter))]
    public async Task<ActionResult> shortURL( UserInput longUrl )
    {
        URL? shortURL = _shorturlrepository.createsShortURL(longUrl);

        if( shortURL != null )
        {
            return Ok(shortURL);
        }
        return BadRequest("Unable To Create Short Url");
    }

    [HttpGet("/{shorturlEndpoint}")]
    public async Task<ActionResult> retrieveRealUrl( string shorturlEndpoint )
    {
        string? longUrl = _shorturlrepository.extractShortURL(shorturlEndpoint);

        if(longUrl != null) 
        {
            return Redirect(longUrl);
        }
        return BadRequest("Invalid Url");
    }
}
