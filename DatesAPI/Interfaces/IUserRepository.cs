using DatesAPI.Models;

namespace DatesAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetails> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserDetails user);
    }
}
