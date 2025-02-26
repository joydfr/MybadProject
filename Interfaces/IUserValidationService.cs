using MvcMyBad.Models;

namespace MvcMyBad.Interfaces
{
    public interface IUserValidationService
    {
        Task<bool> IsPseudoUniqueAsync(string pseudo);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
