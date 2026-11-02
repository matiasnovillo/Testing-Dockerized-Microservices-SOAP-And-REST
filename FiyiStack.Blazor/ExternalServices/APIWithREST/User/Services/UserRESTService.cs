using System.Net;
using System.Net.Http.Json;
using FiyiStack.Blazor.ExternalServices.APIWithREST.User.DTOs;
using FiyiStack.Blazor.ExternalServices.APIWithREST.User.Interfaces;

namespace FiyiStack.Blazor.ExternalServices.APIWithREST.User.Services;

public class UserRESTService(HttpClient httpClient) : IUserRESTService
{
    public async Task<UserDTO> GetUserAsync(int userId)
    {
        using var response = await httpClient.GetAsync($"api/user/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException("No se encontró el usuario solicitado.");
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDTO>()
            ?? throw new InvalidOperationException("El servicio REST devolvió una respuesta vacía.");
    }
}
