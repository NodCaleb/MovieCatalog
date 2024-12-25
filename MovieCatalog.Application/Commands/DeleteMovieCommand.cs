using MediatR;

namespace MovieCatalog.Application.Commands;

public class DeleteMovieCommand : IRequest<bool>
{
	public int Id { get; set; }
}
