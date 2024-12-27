namespace MovieCatalog.Application.Interfaces;

public interface ICachableRequest
{
	string CacheKey { get; }
	TimeSpan CacheDuration { get; }
}
