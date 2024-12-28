using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Infrastructure.Data;
using System.Linq.Expressions;

namespace MovieCatalog.Infrastructure.Repositories;

/// <summary>
/// Repository for movie entities to work with the database
/// </summary>
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

	public async Task<IEnumerable<Movie>> ListMovies(Expression<Func<Movie, bool>> filter, int? skip, int? take, SortBy? sortBy)
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
			case SortBy.Popularity:
				query = query.OrderBy(m => m.Popularity);
				break;
			default:
				query = query.OrderBy(m => m.Id);
				break;
		}

		if (skip.HasValue) query = query.Skip(skip.Value);
		if (take.HasValue) query = query.Take(take.Value);

		return await query.ToListAsync();
	}

	public async Task UpdateMovie(Movie movie)
	{
		_context.Movies.Update(movie);
		await _context.SaveChangesAsync();
	}
}
