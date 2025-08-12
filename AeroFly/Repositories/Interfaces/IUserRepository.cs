using System.Collections.Generic;
using System.Threading.Tasks;
using AeroFly.Controllers.Models;

namespace AeroFly.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);

        Task<User>AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> RegisterUserAsync(User user);

        Task<bool> UpdateUserPasswordAsync(string email, string newPassword);



    }
}
