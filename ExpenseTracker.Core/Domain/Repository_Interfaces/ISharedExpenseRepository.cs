using ExpenseTracker.Core.Domain.Entities;

namespace ExpenseTracker.Core.Domain.Repository_Interfaces
{
    public interface ISharedExpenseRepository
    {
        /// <summary>
        ///  Create a Shared Expense to DB
        /// </summary>
        /// <param name="sharedExpense"></param>
        /// <returns></returns>
        public Task<SharedExpense> CreateSharedExpense(SharedExpense sharedExpense);

        /// <summary>
        /// Get a Shared Expense By Id
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public Task<SharedExpense?> GetSharedExpenseById(Guid sharedExpenseId);

        /// <summary>
        /// Get Shared Expenses of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<SharedExpense>?> GetSharedExpensesOfUser(Guid userId);

        /// <summary>
        /// Get all Shared Expenses
        /// </summary>
        /// <returns></returns>
        public Task<List<SharedExpense>?> GetSharedExpenses();

        /// <summary>
        /// Get a Shared Expense by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<SharedExpense?> GetSharedExpenseByName(string name);

        /// <summary>
        /// Delete a Shared Expense
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public Task<bool> DeleteSharedExpense(Guid sharedExpenseId);
    }
}
