using ASPNETCoreAndXUnit.Services;

namespace ASPNETCoreAndXUnit.IntegrationTests
{
	internal class NegativePrimeService: IPrimeService
	{
		public NegativePrimeService()
		{
		}

		public bool IsPrime(int numberToCheck)
		{
			return false;
		}
	}
}