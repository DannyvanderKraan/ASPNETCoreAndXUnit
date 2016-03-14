using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.TestHost;
using Xunit;

namespace ASPNETCoreAndXUnit.IntegrationTests
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class AspnetCoreAndXUnitShould
	{
		private readonly HttpClient _client;

		public AspnetCoreAndXUnitShould()
		{
			// Arrange
			var server = new TestServer(TestServer.CreateBuilder()
				.UseStartup<Startup>());
			_client = server.CreateClient();
		}

		[Fact]
		public async Task ReturnHelloWorld()
		{
			// Act
			var response = await _client.GetAsync("/");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal("Hello World!",
				responseString);
		}
	}
}
