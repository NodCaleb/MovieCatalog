using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Entities;

namespace MovieCatalog.Infrastructure.Data;

/// <summary>
/// Database context for the movie catalog to work with MS SQL Server
/// </summary>
public class MovieCatalogDbContext : DbContext
{
	public DbSet<Movie> Movies { get; set; }

	public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Movie>(movie =>
		{
			movie.HasKey(m => m.Id);
			movie.Property(m => m.Title).IsRequired().HasMaxLength(255);
			movie.Property(m => m.Overview).HasMaxLength(2000);
			movie.Property(m => m.Genre).HasMaxLength(255);
			movie.Property(m => m.Homepage).HasMaxLength(2048);
			movie.Property(m => m.Director).HasMaxLength(50);
			movie.Property(m => m.Writer).HasMaxLength(50);
			movie.Property(m => m.Language).HasMaxLength(50);
			movie.Property(m => m.Country).HasMaxLength(50);
			movie.Property(m => m.Actors).HasMaxLength(2000);
			movie.Property(m => m.Poster).HasMaxLength(2048);
			movie.OwnsOne(m => m.Budget, budget =>
			{
				budget.Property(b => b.Amount).HasColumnName("BudgetAmount").HasColumnType("decimal(18, 2)");
				budget.Property(b => b.Currency).HasColumnName("BudgetCurrency").HasMaxLength(3);
			});
		});
	}
}
