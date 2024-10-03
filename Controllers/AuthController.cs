using Administration.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUtilisateur_Service _userService; // Suppose que tu as un service pour récupérer l'utilisateur

    public AuthController(IAuthService authService, IUtilisateur_Service userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userService.Authenticate(loginDto.Email, loginDto.Password);
        if (user == null) return Unauthorized("Invalid credentials");

        var token = _authService.GenerateToken(user.ID_Utilisateur.ToString(), user.Role_Utilisateur);
        return Ok(new { Token = token });
    }
}
