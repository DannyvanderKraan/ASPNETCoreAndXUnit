using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreAndXUnit.Middleware;
using Microsoft.AspNet.Http;

namespace ASPNETCoreAndXUnit.IntegrationTests
{
	public class AlternativePrimeCheckerOptions : IPrimeCheckerOptions
	{
		public PathString Path { get; set; }

		public AlternativePrimeCheckerOptions(string path)
		{
			this.Path = path;
		}
	}
}
