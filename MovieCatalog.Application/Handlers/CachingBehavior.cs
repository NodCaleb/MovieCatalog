using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MovieCatalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.Application.Handlers;

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
		// Check if the request type has caching enabled
		if (request is ICachableRequest cachableRequest)
		{
			var cacheKey = cachableRequest.CacheKey;
			if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
			{
				return cachedResponse;
			}

			// Proceed to the next behavior/handler
			var response = await next();

			// Cache the response
			_cache.Set(cacheKey, response, cachableRequest.CacheDuration);
			return response;
		}

		// If not cachable, proceed without caching
		return await next();
	}
}
