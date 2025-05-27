using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // default properties
        /* Id
         * UserName
         * PhoneNumber
         * Email
         * Passwordhash
         */

        // Navigation properties
        public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();

        [JsonIgnore]
        public ICollection<SharedExpense>? SharedExpenses { get; set; } = new List<SharedExpense>();
    }
}
