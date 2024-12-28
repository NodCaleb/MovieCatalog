using MediatR;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Commands;

public class UpdateMovieCommand : MovieCommandBase, IRequest<bool>, ICacheInvalidatingCommand
{
	public int Id { get; set; }
	public string CacheKey => $"Movie-{Id}";
}
