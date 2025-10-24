using Aplication.Models;
using Aplication.Models.Requests;// para createmovierequest
using Aplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movie
        [HttpGet]
        [Authorize(Roles = "Admin, Cliente")]
        public ActionResult<IEnumerable<MovieDto>> GetMovies([FromQuery] bool includeDeleted = false)
        {
            var movie = _movieService.GetAll(includeDeleted)
                .Select(b => new MovieDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    CategoryName = b.Category?.Name
                });

            return Ok(movie);
        }

        // GET: api/Movie/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Cliente")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null || movie.IsDeleted)
                return NotFound();

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Price = movie.Price,
                Stock = movie.Stock,
                CategoryName = movie.Category?.Name
            };

            return Ok(dto);
        }

        // POST: api/Movie
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<MovieDto> CreateMovie([FromBody] CreateMovieRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var dto = _movieService.CreateMovie(request);
            if (dto == null)
                return NotFound();

            return CreatedAtAction(nameof(GetMovie), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var updated = _movieService.Update(id, request);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE lógico: api/Movie
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMovie(int id)
        {
            var deleted = _movieService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }















    }
}
