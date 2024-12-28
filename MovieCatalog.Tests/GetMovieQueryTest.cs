using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MovieCatalog.Application.Queries;

namespace MovieCatalog.Tests
{
	public class GetMovieQueryTest : TestBase
	{
        [Fact]
		public async Task ShouldReturnNull_WhenNoSuchMovieExist()
		{
			var mediator = ServiceProvider.GetRequiredService<IMediator>();
			var query = new GetMovieQuery { Id = 0 };
			var result = await mediator.Send(query);
			Assert.Null(result);
		}

        [Fact]
		public async Task ShouldReturnMovie_WhenMovieExists()
		{
			var mediator = ServiceProvider.GetRequiredService<IMediator>();

			var random = new Random(DateTime.UtcNow.Millisecond);
			var id = random.Next(1, 100);

			var query = new GetMovieQuery { Id = id };
			var result = await mediator.Send(query);
			Assert.NotNull(result);
			Assert.Equal(id, result.Id);
		}
	}
}