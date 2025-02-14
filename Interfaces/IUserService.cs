using MvcMyBad.Models;

namespace MvcMyBad.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetUsersAsync();
        Task<PostUserDto> CreateUserAsync(PostUserDto user);
        Task<bool> EmailExistsAsync(string email);
    }
}