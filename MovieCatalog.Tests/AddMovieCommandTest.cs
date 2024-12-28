using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.Commands;

namespace MovieCatalog.Tests;

/// <summary>
/// Test class for the AddMovieCommand
/// </summary>
public class AddMovieCommandTest : TestBase
{
	[Fact]
	public async Task ShouldAddMovie()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var random = new Random(DateTime.UtcNow.Millisecond);
		var movie = new AddMovieCommand
		{
			Title = $"Movie {random.Next(1, 100)}",
			ReleaseDate = DateTime.UtcNow,
			Genre = "Action",
			Rating = random.Next(1, 10)
		};

		var result = await mediator.Send(movie);
		Assert.NotEqual(0, result);
	}
}
