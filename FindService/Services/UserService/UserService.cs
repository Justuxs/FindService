using FindService.Dto.User;
using FindService.Dto;
using FindService.EF.Context;
using FindService.EF;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FindService.Services.UserService
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse<UserDto>> RegisterAsync(RegisterUserDto registerUserDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerUserDto.Email))
            {
                return new APIResponse<UserDto>("Email is already in use.");
            }

            var user = new User
            {
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname,
                Email = registerUserDto.Email,
                Phone = registerUserDto.Phone,
                PasswordHash = HashPassword(registerUserDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone
            };

            return new APIResponse<UserDto>(userDto);
        }

        public async Task<APIResponse<UserDto>> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null || !VerifyPassword(loginUserDto.Password, user.PasswordHash))
            {
                return new APIResponse<UserDto>("Invalid email or password.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone
            };

            return new APIResponse<UserDto>(userDto);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
