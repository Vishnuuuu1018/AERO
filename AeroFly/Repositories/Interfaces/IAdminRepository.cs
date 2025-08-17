using AeroFly.Controllers.Models;
using AeroFly.DTOs;

namespace AeroFly.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> ValidateAdminAsync(string username, string password);
        string GenerateJwtToken(Admin admin);
        void AddAdmin(Admin admin);
        Task<Admin> GetAdminByIdAsync(int id);
        Task<Admin> UpdateAdminAsync(int adminId, AdminUpdateDto dto);

        // New methods
        Task<bool> UsernameExistsAsync(string username);
        Task AddAdminAsync(Admin admin);
        Task<Admin?> GetAdminByUsernameAsync(string username);
        Task UpdateAdminPasswordAsync(int adminId, string newPassword);
    }
}
