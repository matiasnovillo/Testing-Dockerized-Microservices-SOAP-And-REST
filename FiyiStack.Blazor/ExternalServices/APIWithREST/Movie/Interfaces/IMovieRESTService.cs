using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.DTOs;

namespace FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Interfaces
{
    public interface IMovieRESTService
    {
        public Task<MovieDTO?> GetOneByIdAsync(int movieId);
    }
}
