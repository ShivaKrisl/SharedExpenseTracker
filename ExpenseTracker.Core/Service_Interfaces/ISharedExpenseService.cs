
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
        /// Retrieves shared expenses by ID.
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public Task<SharedExpenseResponse?> GetSharedExpenseById(Guid sharedExpenseId);

        /// <summary>
        /// Retrieves all shared expenses.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<SharedExpenseResponse>> GetSharedExpensesByUserId(Guid userId);

        /// <summary>
        /// Deletes an existing shared expense.
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public Task<bool> DeleteSharedExpense(Guid sharedExpenseId);
    }
}
