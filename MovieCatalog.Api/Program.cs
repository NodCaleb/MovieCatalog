using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.Application.MappingProfiles;
using MovieCatalog.Application.Middleware;
using MovieCatalog.Application.Pipeline;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Infrastructure.Data;
using MovieCatalog.Infrastructure.Data.Seed;
using MovieCatalog.Infrastructure.Extensions;
using MovieCatalog.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Reading configuration from appsettings.json, environment variables, user secrets and command line arguments
var configuration = builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>()
	.AddCommandLine(args)
	.Build();

builder.AddSqlServerDbContext<MovieCatalogDbContext>("MovieCatalog");

builder.AddServiceDefaults();

// Adding services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddAutoMapper(typeof(MovieMappingProfile));
builder.Services.AddAutoMapper(typeof(MovieDataMappingProfile));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddMemoryCache();

builder.Services.AddMediatR(config => {
	config.RegisterServicesFromAssembly(Assembly.Load("MovieCatalog.Application"));
});
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));


builder.Services.AddScoped<MovieCatalogDbContextInitializer>();

builder.Services.AddControllers();

// Adding Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new() { Title = "Movies catalog", Version = "v1" });

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Adding exception handling middleware
app.UseMiddleware<ExceptionsHandler>();

// Initialize the database
await app.InitializeDatabase();

app.Run();
