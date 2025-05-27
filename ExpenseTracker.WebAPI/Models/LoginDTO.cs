using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebAPI.Models
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email should be a valid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
