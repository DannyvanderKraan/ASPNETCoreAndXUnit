using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreAndXUnit.Services;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;


namespace ASPNETCoreAndXUnit.IntegrationTests
{
	public class CompositionRootFixture
	{
		public IServiceProvider ServiceProvider { get; } 
		public string Path { get; }

		public CompositionRootFixture()
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance(new NegativePrimeService()).As<IPrimeService>();
			var container = builder.Build();
			ServiceProvider = container.Resolve<IServiceProvider>();

			Path = "@/checkprime";
		}
	}
}
