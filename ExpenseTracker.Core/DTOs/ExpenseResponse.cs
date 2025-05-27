using ExpenseTracker.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Core.DTOs
{
    public class ExpenseResponse
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

        [Required]
        public Guid UserId { get; set; }

        public string? Username { get; set; }

    }

    public static class ExpenseExtensions
    {
        public static ExpenseResponse ToExpenseResponse(this Expense expense)
        {
            return new ExpenseResponse()
            {
                ExpenseId = expense.ExpenseId,
                Amount = expense.Amount,
                ExpenseType = expense.ExpenseType,
                Reason = expense.Reason,
                DateOfCreation = expense.DateOfCreation,
                UserId = expense.UserId,
                Username = expense.User.UserName,
            };
        }
    }
}
