using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieCatalog.Domain.Entities;
using MovieCatalog.Infrastructure.Data.Seed;
using Newtonsoft.Json;
using System.Reflection;

namespace MovieCatalog.Infrastructure.Data;

/// <summary>
/// Database context initializer, ensures that the database is created, migrations applied and seeded with initial data
/// </summary>
public class MovieCatalogDbContextInitializer
{
	private readonly ILogger<MovieCatalogDbContextInitializer> _logger;
	private readonly IMapper _mapper;
	private readonly MovieCatalogDbContext _context;

	public MovieCatalogDbContextInitializer(MovieCatalogDbContext context, IMapper mapper, ILogger<MovieCatalogDbContextInitializer> logger)
	{
		_context = context;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task Initialize()
	{
		try
		{
			if (_context.Database.EnsureCreated())
			{
				await SeedData();
			}
		}
		catch (Exception e)
		{
			_logger.LogError(e, "An error occurred while initializing the database.");
			throw;
		}
	}

	private async Task SeedData()
	{
		try
		{
			var changed = await SeedMovies();
			if (changed)
			{
				await _context.SaveChangesAsync();
			}
		}
		catch (Exception e)
		{
			_logger.LogError(e, "An error occurred while seeding the database.");
			throw;
		}
	}

	private async Task<bool> SeedMovies()
	{
		var chaged = false;

		if (!await _context.Movies.AnyAsync())
		{
			var filePath = Path.Combine(
				Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
				"Data/Seed/Movies.json");

			var settings = new JsonSerializerSettings
			{
				Converters = { new CustomDateConverter() }
			};

			var moviesData = JsonConvert.DeserializeObject<List<MovieData>>(File.ReadAllText(filePath), settings);

			var movies = moviesData.Select(m => _mapper.Map<Movie>(m));

			_context.Movies.AddRange(movies);

			chaged = true;
		}

		return chaged;
	}
}
