using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.ValueObjects;

namespace MovieCatalog.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public int Popularity { get; set; }
    public DateTime? ReleaseDate { get; set; }
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
