using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Domain.Entities;

namespace ExpenseTracker.Core.DTOs
{
    public class ExpenseRequest
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public double Amount { get; set; }

        [Required]
        public ExpenseType ExpenseType { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        public string? Reason { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }

        public Expense ToExpense()
        {
            return new Expense
            {
                Amount = this.Amount,
                ExpenseType = this.ExpenseType.ToString(),
                Reason = this.Reason,
                UserId = this.UserId
            };
        }
    }
}
