using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MvcMyBad.Data;
using MvcMyBad.Interfaces;
using MvcMyBad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace MvcMyBad.Services
{
    public class UserService : IUserService
    {
        private readonly MybadDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(MybadDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetUserDto>> GetUsersAsync()
        {
            return await _context
                .User.Select(user => new GetUserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Pseudo = user.Pseudo,
                    IsAdmin = user.IsAdmin
                })
                .ToListAsync();
        }

        public async Task<PostUserDto> CreateUserAsync(PostUserDto user)
        {
            var newUser = new UserModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Pseudo = user.Pseudo,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password), // Hash du mot de passe
                PhoneNumber = user.PhoneNumber,
                IsAdmin = false
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<UserModel?> LoginUser(UserLoginDTO loginDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Pseudo == loginDto.Pseudo);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }

            return user;
        }


        public string GenerateJwtToken(UserModel user)
        {
            var issuer = _configuration["JWTConfig:Issuer"];
            var audience = _configuration["JWTConfig:Audience"];
            var secretKey = _configuration["JWTConfig:secret"];
            var expiration = int.Parse(_configuration["JwtConfig:Expiration"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Pseudo),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
