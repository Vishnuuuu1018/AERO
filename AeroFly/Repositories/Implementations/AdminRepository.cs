using AeroFly.Controllers.Models;
using AeroFly.DTOs;
using AeroFly.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AeroFly.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Admin?> ValidateAdminAsync(string username, string password)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        }

        public string GenerateJwtToken(Admin admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public async Task AddAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Admins.AnyAsync(a => a.Username == username);
        }

        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == id);
        }

        public async Task<Admin?> GetAdminByUsernameAsync(string username)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task UpdateAdminPasswordAsync(int adminId, string newPassword)
        {
            var admin = await _context.Admins.FindAsync(adminId);
            if (admin != null)
            {
                admin.Password = newPassword; // ⚠️ consider hashing later
                _context.Admins.Update(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Admin> UpdateAdminAsync(int adminId, AdminUpdateDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == adminId);
            if (admin == null) return null;

            admin.Name = dto.Name;
            admin.Gender = dto.Gender;
            admin.Phone = dto.Phone;
            admin.Age = dto.Age;

            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
