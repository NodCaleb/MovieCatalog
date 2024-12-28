using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.Commands;

namespace MovieCatalog.Tests;

/// <summary>
/// Test class for the DeleteMovieCommand
/// </summary>
public class DeleteMovieCommandTest : TestBase
{
	[Fact]
	public async Task ShouldNotDeleteMovie_WhenNoSuchMovieExist()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var deleteCommand = new DeleteMovieCommand
		{
			Id = 0
		};

		Assert.False(await mediator.Send(deleteCommand));
	}
	[Fact]
	public async Task ShouldDeleteMovie_WhenMovieExists()
	{
		var mediator = ServiceProvider.GetRequiredService<IMediator>();

		var random = new Random(DateTime.UtcNow.Millisecond);
		var id = random.Next(1, 100);

		var deleteCommand = new DeleteMovieCommand
		{
			Id = id
		};

		Assert.True(await mediator.Send(deleteCommand));
	}
}
