using System.ComponentModel.DataAnnotations;
namespace AeroFly.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        public string FullName { get; set; }

        [Required,EmailAddress]
        public string Email{ get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        public string Gender { get; set; }

        public string ContactNumber { get; set; }

        public string Role { get; set; } = "User";
        


    }
}
