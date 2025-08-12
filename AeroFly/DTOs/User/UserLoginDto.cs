﻿using System.ComponentModel.DataAnnotations;

namespace AeroFly.DTOs.User
{
    public class UserLoginDto
    {
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
