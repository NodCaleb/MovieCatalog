using MediatR;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Commands;

/// <summary>
/// Command to update a movie in the catalog
/// </summary>
public class UpdateMovieCommand : MovieCommandBase, IRequest<bool>, ICacheInvalidatingCommand
{
	public int Id { get; set; }
	public string CacheKey => $"Movie-{Id}";
}
