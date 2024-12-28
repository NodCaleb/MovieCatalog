using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.Queries;

namespace MovieCatalog.Tests;

public class ListMoviesQueryTest : TestBase
{
	[Fact]
	public async Task ShouldReturnMovies()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var query = new ListMoviesQuery();
		var result = await mediator.Send(query);
		Assert.NotEmpty(result);
	}
}
