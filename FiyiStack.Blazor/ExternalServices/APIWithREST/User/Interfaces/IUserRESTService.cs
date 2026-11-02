using FiyiStack.Blazor.ExternalServices.APIWithREST.User.DTOs;

namespace FiyiStack.Blazor.ExternalServices.APIWithREST.User.Interfaces;

public interface IUserRESTService
{
    Task<UserDTO> GetUserAsync(int userId);
}
