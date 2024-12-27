using MediatR;
using Microsoft.Extensions.Logging;

namespace MovieCatalog.Application.Handlers;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<TRequest> _logger;

	public LoggingBehavior(ILogger<TRequest> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		try
		{
			_logger.LogTrace($"Before execution for {typeof(TRequest).Name}");

			return await next();
		}
		finally
		{
			_logger.LogTrace($"After execution for {typeof(TRequest).Name}");
		}
	}
}