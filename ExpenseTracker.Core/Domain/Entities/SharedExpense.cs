using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Core.Domain.Entities
{
    public class SharedExpense
    {
        [Key]
        public Guid SharedExpenseId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Shared Name cannot exceed 50 characters")]
        public string? SharedExpenseName { get; set; } = string.Empty;

        [Required]
        public List<string> UserIds { get; set; } = new List<string>();

        [ForeignKey(nameof(CreatedByUser))]
        public Guid CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
    }
}
