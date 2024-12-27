using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.Handlers;
using MovieCatalog.Application.MappingProfiles;
using MovieCatalog.Application.Middleware;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Infrastructure.Data;
using MovieCatalog.Infrastructure.Data.Seed;
using MovieCatalog.Infrastructure.Extensions;
using MovieCatalog.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>()
	.AddCommandLine(args)
	.Build();

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Logging.ClearProviders();
builder.Logging.AddAWSProvider(builder.Configuration.GetAWSLoggingConfigSection());

builder.Services.AddDbContext<MovieCatalogDbContext>(options =>
{
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MovieMappingProfile));
builder.Services.AddAutoMapper(typeof(MovieDataMappingProfile));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddMediatR(config => {
	config.RegisterServicesFromAssembly(Assembly.Load("MovieCatalog.Application"));
});
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddScoped<MovieCatalogDbContextInitializer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionsHandler>();

await app.InitializeDatabase();

app.Run();
