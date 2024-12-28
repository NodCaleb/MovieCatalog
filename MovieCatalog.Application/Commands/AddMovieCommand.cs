using MediatR;

namespace MovieCatalog.Application.Commands;

/// <summary>
/// Command to add a new movie to the catalog
/// </summary>
public class AddMovieCommand : MovieCommandBase, IRequest<int>
{
	
}
