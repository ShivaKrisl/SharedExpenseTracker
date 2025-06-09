using ExpenseTracker.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.DTOs
{
    public class SharedExpenseRequest
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Shared Name cannot exceed 50 characters")]
        public string? SharedExpenseName { get; set; } = string.Empty;

        [Required]
        public List<string> UserIds { get; set; } = new List<string>();

        [Required]
        public Guid CreatedByUserId { get; set; }

        public SharedExpense ToSharedExpense()
        {
            return new SharedExpense
            {
                SharedExpenseName = SharedExpenseName,
                UserIds = UserIds,
                CreatedByUserId = CreatedByUserId
            };
        }
    }
}
