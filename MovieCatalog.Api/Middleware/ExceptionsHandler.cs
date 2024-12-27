namespace MovieCatalog.Application.Middleware;

public class ExceptionsHandler
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionsHandler> _logger;

	public ExceptionsHandler(RequestDelegate next, ILogger<ExceptionsHandler> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception occurred.");
			await HandleExceptionAsync(context, ex);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;

		var response = new
		{
			StatusCode = context.Response.StatusCode,
			Message = "An error occurred while processing your request.",
			Detail = exception.Message
		};

		return context.Response.WriteAsJsonAsync(response);
	}
}
