using AutoMapper;
using MediatR;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Queries;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.Interfaces;
using System.Linq.Expressions;

namespace MovieCatalog.Application.Handlers;

public class ListMoviesQueryHandler : IRequestHandler<ListMoviesQuery, IEnumerable<MovieListDto>>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public ListMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<MovieListDto>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
	{
		var movies = await _movieRepository.ListMovies(request.BuildFilter(), request.Skip, request.Take, request.SortBy);

		return movies.Select(m => _mapper.Map<MovieListDto>(m));
	}
}
