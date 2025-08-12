using AeroFly.Controllers.Models;
using AeroFly.DTOs.User;

using AeroFly.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace AeroFly.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id,User user)
        {
            if (id != user.Id)
            {
                await _userRepository.UpdateUserAsync(user);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var existingUser = await _userRepository.GetUserByEmail(dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email Already Registered");
            }

            var newUser = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Gender = dto.Gender,
                ContactNumber = dto.ContactNumber
                

            };
            var CreatedUser = await _userRepository.RegisterUserAsync(newUser);
            return Ok(CreatedUser);

        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);
            if (user == null)
                return NotFound("User not found");

            user.PasswordHash = dto.Password;
            await _userRepository.UpdateUserAsync(user);

            return Ok("Password updated successfully");
        }



    }
}
