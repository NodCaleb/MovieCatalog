using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.ValueObjects;

namespace MovieCatalog.Application.Dto;

public class MovieDetailsDto : MovieListDto
{
	public Status Status { get; set; }
	public Money? Budget { get; set; }
	public string Homepage { get; set; }
	public float Rating { get; set; }
	public string Genre { get; set; }
	public int Runtime { get; set; }
	public string Director { get; set; }
	public string Writer { get; set; }
	public string Language { get; set; }
	public string Country { get; set; }
	public string Actors { get; set; }
	public string Poster { get; set; }
}
