using System.ComponentModel.DataAnnotations;

namespace AeroFly.Controllers.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.User; 

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Flight> OwnedFlights { get; set; }
    }
    public enum UserRole
    {
        User,
        Admin
    }

}
