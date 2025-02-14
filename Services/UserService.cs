using MvcMyBad.Data;
using Microsoft.EntityFrameworkCore;
using MvcMyBad.Models;
using MvcMyBad.Interfaces;
namespace MvcMyBad.Services
{
    public class UserService : IUserService
    {
        private readonly MybadDbContext _context;

        public UserService(MybadDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetUserDto>> GetUsersAsync()
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
            var NewUser = new UserModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Pseudo = user.Pseudo,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password), // Hash du mot de passe
                PhoneNumber = user.PhoneNumber,
                IsAdmin = false
            };

            _context.User.Add(NewUser);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.User.AnyAsync(u => u.Email == email);
        }
    }
}
