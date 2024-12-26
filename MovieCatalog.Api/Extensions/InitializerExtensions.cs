using MovieCatalog.Infrastructure.Data;

namespace MovieCatalog.Infrastructure.Extensions;

public static class InitializerExtensions
{
	public static async Task InitializeDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		var initializer = scope.ServiceProvider.GetRequiredService<MovieCatalogDbContextInitializer>();

		await initializer.Initialize();
	}
}
