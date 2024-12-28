using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.Interfaces;
using System.Linq.Expressions;

namespace MovieCatalog.Tests.Mock;

/// <summary>
/// Class that mocks the IMovieRepository interface for testing purposes
/// </summary>
internal class MockMovieRepository : IMovieRepository
{
	public async Task AddMovie(Movie movie)
	{
		var random = new Random(DateTime.UtcNow.Millisecond);
		var id = random.Next(1, 100);
		movie.Id = id;
	}

	public async Task DeleteMovie(Movie movie)
	{

	}

	public async Task<Movie?> GetMovie(int id)
	{
		//Assume any movie with id > 0 exists for testing purpuses
		if (id > 0)
		{
			return new Movie
			{
				Id = id,
				Title = "Test Movie",
				ReleaseDate = DateTime.Now,
				Director = "Test Director"
			};
		}

		return null;
	}

	public async Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int? skip, int? take, SortBy? sortBy)
	{
		return new List<Movie>
		{
			new Movie
			{
				Id = 1,
				Title = "Test Movie 1",
				ReleaseDate = DateTime.Now,
				Director = "Test Director 1"
			},
			new Movie
			{
				Id = 2,
				Title = "Test Movie 2",
				ReleaseDate = DateTime.Now,
				Director = "Test Director 2"
			}
		};
	}

	public async Task UpdateMovie(Movie movie)
	{
		
	}
}
