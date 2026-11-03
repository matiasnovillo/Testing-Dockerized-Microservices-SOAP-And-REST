using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.DTOs;
using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Interfaces;
using System.Numerics;

namespace FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Services
{
    public class MovieRESTService : IMovieRESTService
    {
        private readonly HttpClient _httpClient;

        public MovieRESTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MovieDTO?> GetOneByIdAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"/api/Movie/GetOneById/{movieId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MovieDTO>();
        }
    }
}
