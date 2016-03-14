using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace ASPNETCoreAndXUnit.Middleware
{
	public interface IPrimeCheckerOptions
	{
		PathString Path { get; set; }
	}

	public class PrimeCheckerOptions : IPrimeCheckerOptions
	{
	    public PathString Path { get; set; } = @"/checkprime";

    }
}
