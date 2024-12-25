using LinqKit;
using MediatR;
using MovieCatalog.Application.Dto;
using MovieCatalog.Domain.Entities;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieCatalog.Application.Queries;

public class ListMoviesQuery : IRequest<IEnumerable<MovieListDto>>
{
    public string? TitleSearch { get; set; }
    public string? Genre { get; set; }
    public float? MinRating { get; set; }
    public DateTime? ReleasedBefore { get; set; }
    public DateTime? ReleasedAfter { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }

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
