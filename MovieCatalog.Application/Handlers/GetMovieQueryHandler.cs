using AutoMapper;
using MediatR;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Queries;
using MovieCatalog.Domain.Interfaces;

namespace MovieCatalog.Application.Handlers;

public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieDetailsDto>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public GetMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	public async Task<MovieDetailsDto> Handle(GetMovieQuery request, CancellationToken cancellationToken)
	{
		var movie = await _movieRepository.GetMovie(request.Id);
		return _mapper.Map<MovieDetailsDto>(movie);		
	}
}
