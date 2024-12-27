using MovieCatalog.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Application.Commands;

public abstract class MovieCommandBase
{
	[ValidText(isRequired: true, maxLength: 255)]
	public string Title { get; set; }
	[ValidText(isRequired: true, maxLength: 2000)]
	public string Overview { get; set; }
	[Required]
	public int Popularity { get; set; }
	public DateTime? ReleaseDate { get; set; }
	[ValidStatus]
	public string Status { get; set; }
	public decimal? Budget { get; set; }
	public string? BudgetCurrency { get; set; }
	[Url]
	[ValidText(isRequired: true, maxLength: 2048)]
	public string Homepage { get; set; }
	[ValidRating]
	public float? Rating { get; set; }
	[ValidText(isRequired: true, maxLength: 255)]
	public string Genre { get; set; }
	public int? Runtime { get; set; }
	[ValidText(isRequired: true, maxLength: 50)]
	public string Director { get; set; }
	[ValidText(isRequired: true, maxLength: 50)]
	public string Writer { get; set; }
	[ValidText(isRequired: true, maxLength: 50)]
	public string Language { get; set; }
	[ValidText(isRequired: true, maxLength: 50)]
	public string Country { get; set; }
	[ValidText(isRequired: true, maxLength: 2000)]
	public string Actors { get; set; }
	[Url]
	[ValidText(isRequired: true, maxLength: 2048)]
	public string Poster { get; set; }
}
