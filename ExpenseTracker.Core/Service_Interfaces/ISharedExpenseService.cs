
using ExpenseTracker.Core.DTOs;

namespace ExpenseTracker.Core.Service_Interfaces
{
    public interface ISharedExpenseService
    {
        /// <summary>
        /// Creates a new shared expense.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<SharedExpenseResponse> CreateSharedExpense(SharedExpenseRequest request);

        /// <summary>
        /// Retrieves shared expenses.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SharedExpenseResponse>?> GetSharedExpenses();

        /// <summary>
        /// Retrieves all shared expenses of a User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<SharedExpenseResponse>> GetSharedExpensesByUserId(Guid userId);

        /// <summary>
        /// Deletes an existing shared expense.
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> DeleteSharedExpense(Guid sharedExpenseId, Guid userId);

        /// <summary>
        /// Loads the shared Expense 
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ExpenseResponse>?> LoadSharedExpense(Guid sharedExpenseId);
    }
}
