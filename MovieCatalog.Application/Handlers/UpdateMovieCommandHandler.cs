using AutoMapper;
using MediatR;
using MovieCatalog.Application.Commands;
using MovieCatalog.Domain.Interfaces;

namespace MovieCatalog.Application.Handlers;

/// <summary>
/// MediatR Handler for the UpdateMovieCommand
/// </summary>
public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, bool>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public UpdateMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
	{
		var movie = await _movieRepository.GetMovie(request.Id);
		
		if (movie == null) return false;

		movie = _mapper.Map(request, movie);

		await _movieRepository.UpdateMovie(movie);

		return true;
	}
}
