using MediatR;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Queries;

public class GetMovieQuery : IRequest<MovieDetailsDto>, ICachableRequest
{
	public int Id { get; set; }

	public string CacheKey => $"Movie-{Id}";

	public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);
}
