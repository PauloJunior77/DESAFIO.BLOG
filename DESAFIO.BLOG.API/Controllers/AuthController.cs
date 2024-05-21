using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using DESAFIO.BLOG.Application.Services;
using Microsoft.Extensions.Logging;

namespace DESAFIO.BLOG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            _logger.LogInformation("Login endpoint called");
            var properties = new AuthenticationProperties { RedirectUri = "/api/auth/callback" };
            return Challenge(properties, JwtBearerDefaults.AuthenticationScheme);
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            _logger.LogInformation("Register endpoint called");
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/api/auth/callback"
            };
            properties.Items["policy"] = "B2C_1_SignUpSignIn";
            return Challenge(properties, JwtBearerDefaults.AuthenticationScheme);
        }

        [HttpGet("Callback")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Callback()
        {
            _logger.LogInformation("Callback endpoint called");

            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("User is not authenticated");
                return Unauthorized("User is not authenticated.");
            }

            _logger.LogInformation("User is authenticated, processing registration or update");

            var user = await _authService.RegisterOrUpdateUserAsync(User);
            return Ok(user);
        }

        [HttpPost("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Logout endpoint called");

            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoggedOut")
            });

            return Ok();
        }

        [HttpGet("LoggedOut")]
        public IActionResult LoggedOut()
        {
            _logger.LogInformation("LoggedOut endpoint called");
            return Ok("You have been logged out.");
        }
    }
}
