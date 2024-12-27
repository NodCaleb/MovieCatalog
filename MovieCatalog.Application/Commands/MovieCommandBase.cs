using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.ValueObjects;

namespace MovieCatalog.Application.Commands;

public abstract class MovieCommandBase
{
	public string Title { get; set; }
	public string Overview { get; set; }
	public int Popularity { get; set; }
	public DateTime? ReleaseDate { get; set; }
	public string Status { get; set; }
	public decimal? Budget { get; set; }
	public string? BudgetCurrency { get; set; }
	public string Homepage { get; set; }
	public float? Rating { get; set; }
	public string Genre { get; set; }
	public int? Runtime { get; set; }
	public string Director { get; set; }
	public string Writer { get; set; }
	public string Language { get; set; }
	public string Country { get; set; }
	public string Actors { get; set; }
	public string Poster { get; set; }
}
