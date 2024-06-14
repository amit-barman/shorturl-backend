using shorturl.Dto;
using shorturl.Model;

namespace shorturl.Repository;

public interface IShortUrlRepository
{
    URL? createsShortURL(UserInput longUrl);

    string? extractShortURL(string shorturlEndpoint);
}