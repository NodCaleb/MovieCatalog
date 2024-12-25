using AutoMapper;
using MediatR;
using MovieCatalog.Application.Commands;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Interfaces;

namespace MovieCatalog.Application.Handlers;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, int>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public AddMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	public async Task<int> Handle(AddMovieCommand request, CancellationToken cancellationToken)
	{
		var movie = _mapper.Map<Movie>(request);
		await _movieRepository.AddMovie(movie);
		return movie.Id;
	}
}
