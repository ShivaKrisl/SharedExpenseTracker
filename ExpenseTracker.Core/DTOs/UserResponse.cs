using ExpenseTracker.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Core.DTOs
{
    public class UserResponse
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 charcters")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }
    }

    public static class UserExtensions
    {
        public static UserResponse ToUserResponse(this ApplicationUser user)
        {
            return new UserResponse
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
        }
    }
}
