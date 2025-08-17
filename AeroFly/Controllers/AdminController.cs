using AeroFly.Controllers.Models;
using AeroFly.DTOs;
using AeroFly.DTOs.Flight;
using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AeroFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IConfiguration _config;

        public AdminController(
            IAdminRepository adminRepository,
            IFlightRepository flightRepository,
            IConfiguration config)
        {
            _adminRepository = adminRepository;
            _flightRepository = flightRepository;
            _config = config;
        }

        // ----------------- SIGNUP -----------------
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] AdminSignUpDto dto)
        {
            if (await _adminRepository.UsernameExistsAsync(dto.Username))
                return BadRequest("Username already exists");

            var admin = new Admin
            {
                Username = dto.Username,
                Password = dto.Password,
                Name = dto.Name,
                Age = dto.Age,
                Phone = dto.Phone,
                Gender = dto.Gender
            };

            await _adminRepository.AddAdminAsync(admin);
            return Ok("Admin registered successfully");
        }

        // ----------------- LOGIN -----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO loginDto)
        {
            var admin = await _adminRepository.ValidateAdminAsync(loginDto.Username, loginDto.Password);
            if (admin == null)
                return Unauthorized("Invalid credentials");

            var token = _adminRepository.GenerateJwtToken(admin);
            return Ok(new
            {
                token,
                admin = new
                {
                    id = admin.AdminId,
                    username = admin.Username,
                    role = "Admin"
                }
            });
        }

        // ----------------- FORGOT PASSWORD -----------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] AdminForgotPasswordDto dto)
        {
            var admin = await _adminRepository.GetAdminByUsernameAsync(dto.Username);
            if (admin == null)
                return NotFound("Admin not found");

            // Generate temporary password
            var tempPassword = Guid.NewGuid().ToString().Substring(0, 8);
            await _adminRepository.UpdateAdminPasswordAsync(admin.AdminId, tempPassword);

            // In real app, send via Email/SMS instead of returning
            return Ok($"Temporary password: {tempPassword}");
        }

        // ----------------- GET ADMIN DETAILS -----------------
        [Authorize(Roles = "Admin")]
        [HttpGet("details")]
        public async Task<IActionResult> GetAdminDetails()
        {
            var adminIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminIdClaim == null) return Unauthorized();

            var adminId = int.Parse(adminIdClaim);
            var admin = await _adminRepository.GetAdminByIdAsync(adminId);
            if (admin == null) return NotFound();

            return Ok(new
            {
                username = admin.Username,
                Name = admin.Name,
                Gender = admin.Gender,
                Phone = admin.Phone,
                Age = admin.Age
            });
        }

        // ----------------- UPDATE ADMIN -----------------
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminUpdateDto dto)
        {
            var adminIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminIdClaim == null) return Unauthorized();

            var adminId = int.Parse(adminIdClaim);
            var updatedAdmin = await _adminRepository.UpdateAdminAsync(adminId, dto);
            if (updatedAdmin == null) return NotFound();

            return Ok(new
            {
                username = updatedAdmin.Username,
                Name = updatedAdmin.Name,
                Gender = updatedAdmin.Gender,
                Phone = updatedAdmin.Phone,
                Age = updatedAdmin.Age
            });
        }

        // ----------------- DELETE FLIGHT -----------------
        [Authorize(Roles = "Admin")]
        [HttpDelete("flights/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            try
            {
                var adminIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (adminIdClaim == null) return Unauthorized();

                var adminId = int.Parse(adminIdClaim);

                var flight = await _flightRepository.GetFlightByIdAsync(id);
                if (flight == null) return NotFound("Flight not found");
                if (flight.OwnerId != adminId) return Forbid("You can only delete your own flights");

                await _flightRepository.DeleteFlightAsync(id);
                return Ok($"Flight ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting flight: {ex.Message}");
            }
        }
    }
}
