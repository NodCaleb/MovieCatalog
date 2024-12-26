using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Application.Commands;
using MovieCatalog.Application.Dto;
using MovieCatalog.Application.Queries;

namespace MovieCatalog.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public MoviesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Get detailed information about a movie.
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
				return NotFound();
			}

			return Ok(movie);
		}


		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<MovieListDto>), StatusCodes.Status200OK)]
		[Route("")]
		public async Task<IActionResult> ListMovies([FromQuery] ListMoviesQuery query)
		{
			return Ok(await _mediator.Send(query));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[Route("")]
		public async Task<IActionResult> AddMovie([FromBody] AddMovieCommand command)
		{
			var id = await _mediator.Send(command);

			return CreatedAtAction(nameof(GetMovie), new { id }, null);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]	
		[Route("{id}")]
		public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieCommand command)
		{
			command.Id = id;
			if (!await _mediator.Send(command))
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[Route("{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			if (!await _mediator.Send(new DeleteMovieCommand { Id = id }))
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
