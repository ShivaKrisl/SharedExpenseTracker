using ExpenseTracker.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.DTOs
{
    public class SharedExpenseResponse
    {
        [Key]
        public Guid SharedExpenseId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Shared Name cannot exceed 50 characters")]
        public string? SharedExpenseName { get; set; } = string.Empty;

        [Required]
        public List<Guid> UserIds { get; set; } = new List<Guid>();

        [Required]
        public Guid CreatedByUserId { get; set; }

        public User? CreatedByUser { get; set; }
    }

    public static class SharedExpenseExtensions
    {
        public static SharedExpenseResponse ToSharedExpenseResponse(this SharedExpense sharedExpense)
        {
            return new SharedExpenseResponse
            {
                SharedExpenseId = sharedExpense.SharedExpenseId,
                SharedExpenseName = sharedExpense.SharedExpenseName,
                UserIds = sharedExpense.UserIds,
                CreatedByUserId = sharedExpense.CreatedByUserId,
                CreatedByUser = sharedExpense.CreatedByUser,
            };
        }

    }
}
