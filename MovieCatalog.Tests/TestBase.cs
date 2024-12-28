using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.MappingProfiles;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Tests.Mock;
using System.Reflection;

namespace MovieCatalog.Tests;

public abstract class TestBase
{
	protected readonly ServiceProvider ServiceProvider;

    protected TestBase()
	{
		var serviceCollection = new ServiceCollection();

		serviceCollection.AddAutoMapper(typeof(MovieMappingProfile));
		serviceCollection.AddScoped<IMovieRepository, MockMovieRepository>();
		serviceCollection.AddMediatR(config => {
			config.RegisterServicesFromAssembly(Assembly.Load("MovieCatalog.Application"));
		});

		ServiceProvider = serviceCollection.BuildServiceProvider();
	}
}
