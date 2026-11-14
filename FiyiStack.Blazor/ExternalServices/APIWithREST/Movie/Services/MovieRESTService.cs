using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.DTOs;
using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Interfaces;
using FiyiStack.Blazor.Publishers;

namespace FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Services
{
    public class MovieRESTService(HttpClient httpClient) : IMovieRESTService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<MovieDTO?> GetOneByIdAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"/api/Movie/GetOneById/{movieId}");
            
            response.EnsureSuccessStatusCode();

            var movie = await response.Content.ReadFromJsonAsync<MovieDTO>() ??
                throw new InvalidOperationException($"No se encontró la película con ID {movieId}.");

            return movie;
        }
    }
}
