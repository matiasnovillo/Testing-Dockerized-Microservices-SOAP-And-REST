using FiyiStack.APIWithREST.Areas.Client.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FiyiStack.APIWithREST.Areas.User.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpGet("~/api/user/{userId:int}")]
    public ActionResult<UserDTO> GetUser(int userId)
    {
        // Datos de ejemplo: reemplazar por una consulta a la base de datos.
        return Ok(new UserDTO { UserId = 1, Name = "Marcos Novillo" });
    }
}
