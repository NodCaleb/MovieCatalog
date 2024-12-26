using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Infrastructure.Data;
using System.Linq.Expressions;

namespace MovieCatalog.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
	private readonly MovieCatalogDbContext _context;

	public MovieRepository(MovieCatalogDbContext context)
	{
		_context = context;
	}

	public async Task AddMovie(Movie movie)
	{
		_context.Movies.Add(movie);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteMovie(Movie movie)
	{
		_context.Movies.Remove(movie);
		await _context.SaveChangesAsync();
	}

	public async Task<Movie?> GetMovie(int id)
	{
		return await _context.Movies.FindAsync(id);
	}

	public async Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int skip = 0, int take = 10, SortBy sortBy = SortBy.Default)
	{
		var query = _context.Movies.Where(filter).AsNoTracking();

		switch (sortBy)
		{
			case SortBy.Title:
				query = query.OrderBy(m => m.Title);
				break;
			case SortBy.Rating:
				query = query.OrderByDescending(m => m.Rating);
				break;
			case SortBy.ReleaseDate:
				query = query.OrderByDescending(m => m.ReleaseDate);
				break;
			default:
				query = query.OrderBy(m => m.Id);
				break;
		}

		return await query.Skip(skip).Take(take).ToListAsync();
	}

	public async Task UpdateMovie(Movie movie)
	{
		_context.Movies.Update(movie);
		await _context.SaveChangesAsync();
	}
}
