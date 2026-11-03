using FiyiStack.APIWithREST.Areas.Movie.DTOs;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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

            //Esto podria ir en una capa más interna, pero por simplicidad lo dejo aca. Se publica el evento de que se consulto una pelicula
            await _publishEndpoint.Publish<MovieConsultedDTO>(new
            {
                MovieDTO.MovieId,
                MovieDTO.Title
            });

            if (MovieDTO == null)
            {
                return NotFound();
            }

            return Ok(MovieDTO);
        }
    }
}
