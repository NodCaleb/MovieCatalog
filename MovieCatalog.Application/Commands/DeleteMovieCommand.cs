using MediatR;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Commands;

/// <summary>
/// Command to delete a movie from the catalog
/// </summary>
public class DeleteMovieCommand : IRequest<bool>, ICacheInvalidatingCommand
{
	public int Id { get; set; }

	public string CacheKey => $"Movie-{Id}";
}
