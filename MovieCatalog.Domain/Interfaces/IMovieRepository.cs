using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using System.Linq.Expressions;

namespace MovieCatalog.Domain.Interfaces;

public interface IMovieRepository
{
	Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int skip = 0, int take = 10, SortBy sortBy = SortBy.Default);
	Task<Movie?> GetMovie(int id);
	Task AddMovie(Movie movie);
	Task UpdateMovie(Movie movie);
	Task DeleteMovie(Movie movie);
}
