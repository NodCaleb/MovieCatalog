using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.Commands;

namespace MovieCatalog.Tests;

public class UpdateMovieCommandTest : TestBase
{
	[Fact]
	public async Task ShouldNotUpdateMovie_WhenNoSuchMovieExist()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var updateCommand = new UpdateMovieCommand
		{
			Id = 0,
			Title = "Updated Movie",
			ReleaseDate = DateTime.Now,
			Director = "Updated Director"
		};

		Assert.False(await mediator.Send(updateCommand));
	}
	[Fact]
	public async Task ShouldUpdateMovie_WhenMovieExists()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var random = new Random(DateTime.UtcNow.Millisecond);
		var id = random.Next(1, 100);

		var updateCommand = new UpdateMovieCommand
		{
			Id = id,
			Title = "Updated Movie",
			ReleaseDate = DateTime.Now,
			Director = "Updated Director"
		};

		Assert.True(await mediator.Send(updateCommand));
	}
}
