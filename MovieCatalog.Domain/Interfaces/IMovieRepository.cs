using MovieCatalog.Domain.Entities;
using System.Linq.Expressions;

namespace MovieCatalog.Domain.Interfaces;

public interface IMovieRepository
{
	Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int skip = 0, int take = 10);
	Task<Movie> GetMovie(int id);
	Task AddMovie(Movie movie);
	Task<bool> UpdateMovie(Movie movie);
	Task<bool> DeleteMovie(int id);
}
