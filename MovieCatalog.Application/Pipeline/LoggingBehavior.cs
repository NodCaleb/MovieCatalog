using MediatR;
using Microsoft.Extensions.Logging;

namespace MovieCatalog.Application.Pipeline;

/// <summary>
/// Pipeline behavior for trace logging queries and commands execution and log exceptions if they occur
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
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
            _logger.LogTrace($"Before execution of {typeof(TRequest).Name}");

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error during execution of {typeof(TRequest).Name}");
            throw;
        }
        finally
        {
            _logger.LogTrace($"After execution of {typeof(TRequest).Name}");
        }
    }
}