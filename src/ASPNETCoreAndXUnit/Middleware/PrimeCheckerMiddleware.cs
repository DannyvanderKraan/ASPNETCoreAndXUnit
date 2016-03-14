using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreAndXUnit.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace ASPNETCoreAndXUnit.Middleware
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class PrimeCheckerMiddleware
    {
        private readonly RequestDelegate _next;
	    private IPrimeCheckerOptions _options;
	    private IPrimeService _primeService;

	    public PrimeCheckerMiddleware(RequestDelegate next,
			IPrimeCheckerOptions options,
			IPrimeService primeService)
		{
			if (next == null)
			{
				throw new ArgumentNullException(nameof(next));
			}
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}
			if (primeService == null)
			{
				throw new ArgumentNullException(nameof(primeService));
			}

			_next = next;
			_options = options;
			_primeService = primeService;
		}

		public async Task Invoke(HttpContext httpContext)
        {

			HttpRequest request = httpContext.Request;
			if (!request.Path.HasValue ||
				request.Path != _options.Path)
			{
				await _next.Invoke(httpContext);
			}
			else
			{
				int numberToCheck;
				if (int.TryParse(request.QueryString.Value.Replace("?", ""), out numberToCheck))
				{
					if (_primeService.IsPrime(numberToCheck))
					{
						await httpContext.Response.WriteAsync($"{numberToCheck} is prime!");
					}
					else
					{
						await httpContext.Response.WriteAsync($"{numberToCheck} is NOT prime!");
					}
				}
				else
				{
					await httpContext.Response.WriteAsync($"Pass in a number to check in the form {_options.Path}?5");
				}
			}
		}
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PrimeCheckerMiddlewareExtensions
    {
        public static IApplicationBuilder UsePrimeCheckerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PrimeCheckerMiddleware>();
        }
    }
}
