using Microsoft.AspNetCore.Mvc;
using TMA.Api.Model;
using TMA.Api.Repository;

namespace TMA.Web.Controllers
{
    
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

   
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userRepository.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var claims = await _userRepository.GetUserClaimsAsync(user);
            var token = _tokenRepository.GenerateToken(user, claims);

            return Ok(new { TokenResult = token });
        }
    }



}
