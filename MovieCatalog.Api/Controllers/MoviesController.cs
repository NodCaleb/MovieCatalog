using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Application.Commands;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Queries;

namespace MovieCatalog.Api.Controllers;

/// <summary>
/// Controller for managing movies in the catalog
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<MoviesController> _logger;

	public MoviesController(IMediator mediator, ILogger<MoviesController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	/// <summary>
	/// Get detailed information about a movie
	/// </summary>
	/// <param name="id">Movie id</param>
	/// <response code="200">Returns movie details</response>
	/// <response code="404">If there is no movie with this id</response>
	[HttpGet]
	[ProducesResponseType(typeof(MovieDetailsDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[Route("{id}")]
	public async Task<IActionResult> GetMovie(int id)
	{
		var movie = await _mediator.Send(new GetMovieQuery { Id = id });

		if (movie == null)
		{
			return NotFound(new { message = $"Movie with ID {id} not found or not accessible", statusCode = 404 });
		}

		return Ok(movie);
	}

	/// <summary>
	/// Get list of movies
	/// </summary>
	/// <response code="200">Returns list of movies</response>
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<MovieListDto>), StatusCodes.Status200OK)]
	[Route("")]
	public async Task<IActionResult> ListMovies([FromQuery] ListMoviesQuery query)
	{
		return Ok(await _mediator.Send(query));
	}

	/// <summary>
	/// Add new movie to the catalog
	/// </summary>
	/// <param name="command"></param>
	/// <response code="201">Returns response with new movie id</response>
	/// <response code="400">In case of invalid data</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[Route("")]
	public async Task<IActionResult> AddMovie([FromBody] AddMovieCommand command)
	{
		var id = await _mediator.Send(command);

		return CreatedAtAction(nameof(GetMovie), new { id }, null);
	}

	/// <summary>
	/// Update existing movie details
	/// </summary>
	/// <param name="id">Movie id</param>
	/// <response code="204">In case of successful update</response>
	/// <response code="400">In case of invalid data</response>
	/// <response code="404">If there is no movie with this id</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]	
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[Route("{id}")]
	public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieCommand command)
	{
		command.Id = id;
		if (!await _mediator.Send(command))
		{
			return NotFound(new { message = $"Movie with ID {id} not found or not accessible", statusCode = 404 });
		}

		return NoContent();
	}

	/// <summary>
	/// Delete movie from the catalog
	/// </summary>
	/// <param name="id">Movie id</param>
	/// <response code="204">In case of successful deletion</response>
	/// <response code="404">If there is no movie with this id</response>
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[Route("{id}")]
	public async Task<IActionResult> DeleteMovie(int id)
	{
		if (!await _mediator.Send(new DeleteMovieCommand { Id = id }))
		{
			return NotFound(new { message = $"Movie with ID {id} not found or not accessible", statusCode = 404 });
		}

		return NoContent();
	}
}
