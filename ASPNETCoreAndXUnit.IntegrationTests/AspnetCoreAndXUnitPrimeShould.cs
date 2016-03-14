using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASPNETCoreAndXUnit.Middleware;
using ASPNETCoreAndXUnit.Services;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ASPNETCoreAndXUnit.IntegrationTests
{
	public class AspnetCoreAndXUnitPrimeShould: IClassFixture<CompositionRootFixture>
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;
		private readonly CompositionRootFixture _fixture;

		public AspnetCoreAndXUnitPrimeShould(CompositionRootFixture fixture) 
		{
			// Arrange
			_fixture = fixture;
			_server = new TestServer(TestServer.CreateBuilder(null, app =>
			{
				app.UsePrimeCheckerMiddleware();
			},
				services =>
				{
					services.AddSingleton<IPrimeService, NegativePrimeService>();
					services.AddSingleton<IPrimeCheckerOptions>(_ => new AlternativePrimeCheckerOptions(_fixture.Path));
				}));
			_client = _server.CreateClient();
		}

		private async Task<string> GetCheckPrimeResponseString(
			string querystring = "")
		{
			string request = "/checkprime";
			if (!String.IsNullOrEmpty(querystring))
			{
				request += "?" + querystring;
			}
			var response = await _client.GetAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		[Fact]
		public async Task ReturnInstructionsGivenEmptyQueryString()
		{
			// Act
			var responseString = await GetCheckPrimeResponseString();

			// Assert
			Assert.Equal("Pass in a number to check in the form /checkprime?5",
				responseString);
		}
		[Fact]
		public async Task ReturnPrimeGiven5()
		{
			// Act
			var responseString = await GetCheckPrimeResponseString("5");

			// Assert
			Assert.Equal("5 is prime!",
				responseString);
		}

		[Fact]
		public async Task ReturnNotPrimeGiven6()
		{
			// Act
			var responseString = await GetCheckPrimeResponseString("6");

			// Assert
			Assert.Equal("6 is NOT prime!",
				responseString);
		}
	}
}
