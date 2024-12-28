using MediatR;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Commands;

public class DeleteMovieCommand : IRequest<bool>, ICacheInvalidatingCommand
{
	public int Id { get; set; }

	public string CacheKey => $"Movie-{Id}";
}
