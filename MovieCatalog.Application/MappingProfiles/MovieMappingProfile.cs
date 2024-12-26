﻿using AutoMapper;
using MovieCatalog.Application.Commands;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Extensions;
using MovieCatalog.Domain.Entities;

namespace MovieCatalog.Application.MappingProfiles;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieListDto>();
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dto => dto.Status, opt => opt.MapFrom(movie => movie.Status.GetEnumDescription()));
        CreateMap<AddMovieCommand, Movie>();
        CreateMap<UpdateMovieCommand, Movie>()
            .ForMember(movie => movie.Id, opt => opt.Ignore());
    }
}
