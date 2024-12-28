using AutoMapper;
using MediatR;
using MovieCatalog.Application.Commands;
using MovieCatalog.Domain.Interfaces;

namespace MovieCatalog.Application.Handlers;

/// <summary>
/// MediatR Handler for the DeleteMovieCommand
/// </summary>
public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public DeleteMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
	{
		var movie = await _movieRepository.GetMovie(request.Id);

		if (movie == null) return false;

		await _movieRepository.DeleteMovie(movie);

		return true;
	}
}
