using Microsoft.EntityFrameworkCore;
using MvcMyBad.Interfaces;
using MvcMyBad.Data;

namespace MvcMyBad.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly MybadDbContext _context;

        public UserValidationService(MybadDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPseudoUniqueAsync(string pseudo)
        {
            return !await _context.User.AnyAsync(u => u.Pseudo == pseudo);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _context.User.AnyAsync(u => u.Email == email);
        }
    }
}
