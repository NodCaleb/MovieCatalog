using MediatR;

namespace MovieCatalog.Application.Commands;

public class AddMovieCommand : MovieCommandBase, IRequest<int>
{
	
}
