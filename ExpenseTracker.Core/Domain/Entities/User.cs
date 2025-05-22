using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 charcters")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        // Navigation properties
        public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();
        public ICollection<SharedExpense>? SharedExpenses { get; set; } = new List<SharedExpense>();
    }
}
