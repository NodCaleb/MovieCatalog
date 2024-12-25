using MediatR;
using MovieCatalog.Application.Dto;

namespace MovieCatalog.Application.Queries;

public class GetMovieQuery : IRequest<MovieDetailsDto>
{
	public int Id { get; set; }
}
