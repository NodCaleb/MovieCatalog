using LinqKit;
using MediatR;
using MovieCatalog.Application.Dto;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Enums;
using System.Linq.Expressions;

namespace MovieCatalog.Application.Queries;

public class ListMoviesQuery : IRequest<IEnumerable<MovieListDto>>
{
    public string? TitleSearch { get; set; }
    public string? Genre { get; set; }
    public string? Director { get; set; }
    public string? Actor { get; set; }
    public string? Writer { get; set; }
    public float? MinRating { get; set; }
    public DateTime? ReleasedBefore { get; set; }
    public DateTime? ReleasedAfter { get; set; }
	public int Skip { get; set; } = 0;
	public int Take { get; set; } = 100;
    public SortBy? SortBy { get; set; }

    public Expression<Func<Movie, bool>> BuildFilter()
    {
		var filter = PredicateBuilder.New<Movie>(true);

		if (!string.IsNullOrWhiteSpace(TitleSearch))
		{
			filter = filter.And(m => m.Title.ToLower().Contains(TitleSearch.ToLower()));
		}

		if (!string.IsNullOrWhiteSpace(Genre))
		{
			filter = filter.And(m => m.Genre.ToLower().Contains(Genre.ToLower()));
		}

		if (!string.IsNullOrWhiteSpace(Director))
		{
			filter = filter.And(m => m.Director.ToLower().Contains(Director.ToLower()));
		}

		if (!string.IsNullOrWhiteSpace(Actor))
		{
			filter = filter.And(m => m.Actors.ToLower().Contains(Actor.ToLower()));
		}

		if (!string.IsNullOrWhiteSpace(Writer))
		{
			filter = filter.And(m => m.Writer.ToLower().Contains(Writer.ToLower()));
		}

		if (MinRating.HasValue)
		{
			filter = filter.And(m => m.Rating >= MinRating);
		}

		if (ReleasedBefore.HasValue)
		{
			filter = filter.And(m => m.ReleaseDate <= ReleasedBefore);
		}

		if (ReleasedAfter.HasValue)
		{
			filter = filter.And(m => m.ReleaseDate >= ReleasedAfter);
		}

		return filter;
	}
}
