using HotelMotorApi.Interfaces;
using HotelMotorShared.Dtos.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (result == null)
                return BadRequest(new { status = 400, message = "Correo ya registrado" });

            return Ok(new { status = 200, message = "Registro exitoso", data = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized(new { status = 401, message = "Credenciales inválidas" });

            return Ok(new { status = 200, message = "Inicio de sesión exitoso", data = result });
        }
    }
}
