using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MovieCatalog.Application.Interfaces;

namespace MovieCatalog.Application.Pipeline;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;

    public CachingBehavior(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Check if the request type allows caching
        if (request is ICachableQuery cachableQuery)
        {
            var cacheKey = cachableQuery.CacheKey;
            if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }

            // Proceed to the next behavior/handler
            var response = await next();

            // Cache the response
            _cache.Set(cacheKey, response, cachableQuery.CacheDuration);
            return response;
        }

        //Check if the request type requires invalidating the cache
        if (request is ICacheInvalidatingCommand invalidatingCommand)
		{
			var cacheKey = invalidatingCommand.CacheKey;
			_cache.Remove(cacheKey);
		}

        // If not cachable, proceed without caching
        return await next();
    }
}
