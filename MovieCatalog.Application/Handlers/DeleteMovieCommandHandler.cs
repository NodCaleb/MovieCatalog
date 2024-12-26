using AutoMapper;
using MediatR;
using MovieCatalog.Application.Commands;
using MovieCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.Application.Handlers;

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
