using AeroFly.Controllers.Models;
using AeroFly.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AeroFly.Repositories.Implementations
{

    public class UserRepo : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }
        public async Task<User> UpdateUserAsync(User user)
        {
            var existingemp = await _context.Users.FindAsync(user.Id);
            if (existingemp != null)
            {
                existingemp.FullName = user.FullName;
                existingemp.Email = user.Email;
                existingemp.PasswordHash = user.PasswordHash;
                existingemp.Gender = user.Gender;
                existingemp.ContactNumber = user.ContactNumber;
                existingemp.Role = user.Role;

                await _context.SaveChangesAsync();
                return existingemp;
            }
            return null;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var delemp = await _context.Users.FindAsync(id);
            if (delemp != null)
            {
                _context.Remove(delemp);
                await _context.SaveChangesAsync();
                return delemp;
            }
            return null;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.PasswordHash = newPassword; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
