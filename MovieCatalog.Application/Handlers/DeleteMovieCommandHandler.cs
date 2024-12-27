using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MovieCatalog.Application.Commands;
using MovieCatalog.Domain.Interfaces;

namespace MovieCatalog.Application.Handlers;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
{
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;
	private readonly IMemoryCache _cache;

	public DeleteMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper, IMemoryCache cache)
	{
		_movieRepository = movieRepository;
		_mapper = mapper;
		_cache = cache;
	}

	public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
	{
		var movie = await _movieRepository.GetMovie(request.Id);

		if (movie == null) return false;

		await _movieRepository.DeleteMovie(movie);

		_cache.Remove($"Movie-{request.Id}");

		return true;
	}
}
