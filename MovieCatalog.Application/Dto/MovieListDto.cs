namespace MovieCatalog.Application.Dto;

public class MovieListDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Overview { get; set; }
	public int Popularity { get; set; }
	public DateTime? ReleaseDate { get; set; }
}
