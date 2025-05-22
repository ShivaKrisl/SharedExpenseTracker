using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Domain.Entities;

namespace ExpenseTracker.Core.DTOs
{
    public class UserRequest
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 charcters")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        public User ToUser()
        {
            return new User
            {
                Username = this.Username,
                Email = this.Email
            };
        }
    }
}
