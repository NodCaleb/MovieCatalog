using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.MappingProfiles;
using MovieCatalog.Domain.Interfaces;
using MovieCatalog.Infrastructure.Data;
using MovieCatalog.Infrastructure.Data.Seed;
using MovieCatalog.Infrastructure.Extensions;
using MovieCatalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>()
	.AddCommandLine(args)
	.Build();

// Add services to the container.
builder.Services.AddDbContext<MovieCatalogDbContext>(options =>
{
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MovieMappingProfile));
builder.Services.AddAutoMapper(typeof(MovieDataMappingProfile));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddMediatR(cfg => {
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddScoped<MovieCatalogDbContextInitializer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.InitializeDatabase();

app.Run();
