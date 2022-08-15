﻿using System.ComponentModel.DataAnnotations;

namespace WebApAutores.DTOs
{
    public class CredencialesUsuarios
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
