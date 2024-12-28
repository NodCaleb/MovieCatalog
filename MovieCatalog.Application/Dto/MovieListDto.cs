namespace MovieCatalog.Application.Dto;

/// <summary>
/// Data transfer object with brief movie details, used for displaying list of movies
/// </summary>
public class MovieListDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Overview { get; set; }
	public int Popularity { get; set; }
	public DateTime? ReleaseDate { get; set; }
}
