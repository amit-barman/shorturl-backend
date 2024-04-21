using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace shorturl.Authentication;

public class AuthFilter : IAuthorizationFilter
{
	private readonly IConfiguration Configuration;

    public AuthFilter(IConfiguration configuration)
    {
        Configuration = configuration;
    }

	public void OnAuthorization(AuthorizationFilterContext context)
	{
		if(!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.KeyHeader, out var extractedKey))
		{
			context.Result = new UnauthorizedObjectResult("Api key Missing");
			return;
		}

		var apiKey = Configuration.GetValue<string>(AuthConstants.ApiKey);

		if(!apiKey.Equals(extractedKey))
		{
			context.Result = new UnauthorizedObjectResult("Invalid Api key");
			return;
		}
	}
}