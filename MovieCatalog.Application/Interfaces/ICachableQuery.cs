namespace MovieCatalog.Application.Interfaces;

public interface ICachableQuery
{
	string CacheKey { get; }
	TimeSpan CacheDuration { get; }
}
