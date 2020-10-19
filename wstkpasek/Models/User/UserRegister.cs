using System;
using System.ComponentModel.DataAnnotations;

namespace wstkpasek.Models.User
{
    public class UserRegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }

    }
}
