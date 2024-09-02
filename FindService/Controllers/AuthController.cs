using FindService.Dto;
using FindService.Dto.User;
using FindService.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserDto">The registration details for the user.</param>
        /// <returns>An API response indicating success or failure.</returns>
        /// <response code="200">Returns the newly registered user's details.</response>
        /// <response code="400">If the registration data is invalid or the email is already in use.</response>
        ///     /// <remarks>
        /// **Example Request:**
        ///
        /// POST /api/auth/register
        ///
        /// ```json
        /// {
        ///   "name": "John",
        ///   "surname": "Doe",
        ///   "email": "johndoe@example.com",
        ///   "phone": "1234567890",
        ///   "password": "StrongPassword123!"
        /// }
        /// ```
        /// </remarks>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registers a new user", Description = "Creates a new user account with the provided registration details.")]
        [SwaggerResponse(200, "Returns the newly registered user's details.", typeof(APIResponse<UserDto>))]
        [SwaggerResponse(400, "If the registration data is invalid or the email is already in use.", typeof(APIResponse<UserDto>))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await _userService.RegisterAsync(registerUserDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="loginUserDto">The login credentials of the user.</param>
        /// <returns>An API response with the user's details if successful.</returns>
        /// <response code="200">Returns the logged-in user's details.</response>
        /// <response code="401">If the login credentials are incorrect.</response>
        /// <remarks>
        /// **Example Request:**
        ///
        /// POST /api/auth/login
        ///
        /// ```json
        /// {
        ///   "email": "johndoe@example.com",
        ///   "password": "StrongPassword123!"
        /// }
        /// ```
        /// </remarks>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Logs in an existing user", Description = "Authenticates a user with the provided login credentials.")]
        [SwaggerResponse(200, "Returns the logged-in user's details.", typeof(APIResponse<UserDto>))]
        [SwaggerResponse(401, "If the login credentials are incorrect.", typeof(APIResponse<UserDto>))]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _userService.LoginAsync(loginUserDto);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
