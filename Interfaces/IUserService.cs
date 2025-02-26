using MvcMyBad.Models;

namespace MvcMyBad.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetUsersAsync();
        Task<PostUserDto> CreateUserAsync(PostUserDto user);
        Task<UserModel?> LoginUser(UserLoginDTO loginDto);
    }
}
