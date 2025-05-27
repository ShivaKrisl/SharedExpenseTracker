using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.Domain.Entities
{
    public class Expense
    {
        [Key]
        public Guid ExpenseId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public double Amount { get; set; }

        [Required]
        public string? ExpenseType { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        public string? Reason { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(this.User))]
        public Guid UserId { get; set; }

        // Navigation property
        public ApplicationUser? User { get; set; }
    }
}
