using DatesAPI.Interfaces;
using DatesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatesAPI.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserDetailsContext _context;

        public UserRepository(UserDetailsContext context)
        {
            _context = context;
        }

        public async Task<UserDetails> GetUserByEmailAsync(string email)
        {
            return await _context.UserDetails
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(UserDetails user)
        {
            await _context.UserDetails.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
