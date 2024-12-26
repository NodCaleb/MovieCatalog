using MediatR;

namespace MovieCatalog.Application.Commands;

public class UpdateMovieCommand : MovieCommandBase, IRequest<bool>
{
	public int Id { get; set; }
}
