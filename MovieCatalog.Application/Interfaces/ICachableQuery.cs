namespace MovieCatalog.Application.Interfaces;

/// <summary>
/// Represents a query that produces the result that can be cached
/// </summary>
public interface ICachableQuery
{
	string CacheKey { get; }
	TimeSpan CacheDuration { get; }
}
