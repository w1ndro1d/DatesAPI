using DatesAPI.Interfaces;
using DatesAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatesAPI.Services
{
    public class UserService: IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IConfiguration configuration, IUserRepository userRepository, IPasswordHasher passwordHasher = null)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception($"No account found for {email}! Please sign up first.");
            }

            if (!_passwordHasher.VerifyHashedPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials!");
            }

            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(UserDetails userRegistration)
        {
            var user = await _userRepository.GetUserByEmailAsync(userRegistration.Email);
            if (user != null)
            {
                throw new Exception("User already exists!");
            }

            var newUser = new UserDetails
            {
                Email = userRegistration.Email,
                PasswordHash = _passwordHasher.HashPassword(userRegistration.PasswordHash),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(newUser);
        }

        private string GenerateJwtToken(UserDetails user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}