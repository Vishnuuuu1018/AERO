using System.ComponentModel.DataAnnotations;
namespace AeroFly.DTOs.User
{
    public class UserRegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public string Gender { get; set; }

        public string ContactNumber { get; set; }
    }
}
