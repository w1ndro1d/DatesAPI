using DatesAPI.Models;

namespace DatesAPI.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(string email, string password);
        Task RegisterAsync(UserDetails userRegistration);
    }
}
