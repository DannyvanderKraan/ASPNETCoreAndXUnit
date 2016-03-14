using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreAndXUnit.Services
{
	public interface IPrimeService
	{
		bool IsPrime(int numberToCheck);
	}

	public class PrimeService : IPrimeService
	{
		public bool IsPrime(int numberToCheck)
		{
			//I don't actually care about this method.
			return true;
		}
	}
}
