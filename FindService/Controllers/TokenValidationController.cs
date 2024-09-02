using FindService.Dto;
using FindService.Dto.JwtToken;
using FindService.Services.AuthService;
using FindService.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenValidationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public TokenValidationController(IConfiguration configuration, JwtService jwtService)
        {
            _configuration = configuration;
            _jwtService = jwtService;

        }

        /// <summary>
        /// Validates the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>A response indicating whether the token is valid or not.</returns>
        /// <remarks>
        /// **Example Request:**
        ///
        /// POST /api/tokenvalidation/validate
        ///
        /// ```json
        /// {
        ///   "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjMmZmYjUwNi0yZTliLTQyZjMtOTBiNy0wNTA1MzUyZjRiZGQiLCJlbWFpbCI6ImpvaG5kb2VAZXhhbXBsZS5jb20iLCJqdGkiOiJjMTEzZjMwYy1iMGEzLTRkY2UtODg4Ny1iYjIxNjQzMWVmYjkiLCJleHAiOjE3MjUyODQzNzMsImlzcyI6IkZpbmRTZXJ2aWNlIiwiYXVkIjoiRmluZFNlcnZpY2VVc2VyIn0.FkgfE03_IdbVMGywMbcmrl6c8bMl-_d0AMTuWHYKQ18"
        /// }
        /// ```
        /// </remarks>
        [HttpPost("validate")]
        [AllowAnonymous]
        public IActionResult ValidateToken([FromBody] TokenRequestDto tokenRequest)
        {
            var token = tokenRequest.Token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new APIResponse<string>("Token is required."));
            }

            var validationParameters = _jwtService.GetValidationParameters();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    return Ok(new APIResponse<string>("Token is valid."));
                }
                else
                {
                    return Unauthorized(new APIResponse<string>("Invalid token."));
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new APIResponse<string>($"Token validation failed: {ex.Message}"));
            }
        }

        /// <summary>
        /// Validates the JWT token provided in the Authorization header.
        /// </summary>
        /// <returns>A response indicating whether the token is valid or not.</returns>
        /// <remarks>
        /// **Example Request:**
        ///
        /// GET /api/tokenvalidation/validate
        ///
        /// Authorization: Bearer your-jwt-token-here
        /// </remarks>
        [HttpGet("validate")]
        [AllowAnonymous]
        public IActionResult ValidateTokenFromHeader()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new APIResponse<string>("Authorization header is missing."));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return BadRequest(new APIResponse<string>("Invalid Authorization header format."));
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var validationParameters =  _jwtService.GetValidationParameters();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Optionally, you can check claims or other token properties here
                    return Ok(new APIResponse<string>("Token is valid."));
                }
                else
                {
                    return Unauthorized(new APIResponse<string>("Invalid token."));
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new APIResponse<string>($"Token validation failed: {ex.Message}"));
            }
        }
    }
}
