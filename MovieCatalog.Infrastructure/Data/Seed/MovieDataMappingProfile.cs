using AutoMapper;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.ValueObjects;

namespace MovieCatalog.Infrastructure.Data.Seed;

public class MovieDataMappingProfile : Profile
{
	public MovieDataMappingProfile()
	{
		CreateMap<MovieData, Movie>()
			.ForMember(movie => movie.Budget, opt => opt.MapFrom(data => data.Budget.HasValue && !string.IsNullOrEmpty(data.BudgetCurrency) ? new Money(data.Budget.Value, data.BudgetCurrency) : null));
	}
}
