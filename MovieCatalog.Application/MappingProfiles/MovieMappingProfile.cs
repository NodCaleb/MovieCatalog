using AutoMapper;
using MovieCatalog.Application.Commands;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Extensions;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Domain.ValueObjects;

namespace MovieCatalog.Application.MappingProfiles;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieListDto>();
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dto => dto.Status, opt => opt.MapFrom(movie => movie.Status.GetEnumDescription()));
        CreateMap<AddMovieCommand, Movie>()
			.ForMember(movie => movie.Budget, opt => opt.MapFrom(data => data.Budget.HasValue && !string.IsNullOrEmpty(data.BudgetCurrency) ? new Money(data.Budget.Value, data.BudgetCurrency) : null));
		CreateMap<UpdateMovieCommand, Movie>()
			.ForMember(movie => movie.Budget, opt => opt.MapFrom(data => data.Budget.HasValue && !string.IsNullOrEmpty(data.BudgetCurrency) ? new Money(data.Budget.Value, data.BudgetCurrency) : null))
			.ForMember(movie => movie.Id, opt => opt.Ignore());
    }
}
