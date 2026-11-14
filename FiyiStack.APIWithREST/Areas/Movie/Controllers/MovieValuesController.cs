using FiyiStack.APIWithREST.Areas.Movie.DTOs;
using FiyiStack.APIWithREST.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FiyiStack.APIWithREST.Areas.Movie.Controllers
{
    [ApiController]
    public class MovieValuesController(IPublishEndpoint publishEndpoint) : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [HttpGet("api/Movie/GetOneById/{id}")]
        public async Task<ActionResult<MovieDTO>> GetOneById(int id)
        {

            //Aca se haria una consulta a la base de datos para obtener la pelicula por id
            MovieDTO MovieDTO = new() { MovieId = id, Title = "Los 3 chiflados" };

            await _publishEndpoint.Publish(new MovieConsulted
            {
                MovieId = MovieDTO.MovieId,
                Title = MovieDTO.Title
            });

            if (MovieDTO == null)
            {
                return NotFound();
            }

            return Ok(MovieDTO);
        }
    }
}
