using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using System.Linq.Expressions;

namespace MovieCatalog.Domain.Interfaces;

public interface IMovieRepository
{
	Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int? skip, int? take, SortBy? sortBy);
	Task<Movie?> GetMovie(int id);
	Task AddMovie(Movie movie);
	Task UpdateMovie(Movie movie);
	Task DeleteMovie(Movie movie);
}
