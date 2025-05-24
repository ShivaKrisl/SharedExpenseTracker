using ExpenseTracker.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Service_Interfaces
{
    public interface IExpenseService
    {
        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expenseRequest"></param>
        /// <returns></returns>
        public Task<ExpenseResponse> CreateExpense(ExpenseRequest expenseRequest);

        /// <summary>
        /// Retrieves expense by Id.
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public Task<ExpenseResponse?> GetExpenseById(Guid expenseId);

        /// <summary>
        /// Retrieves all expenses of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ExpenseResponse>> GetExpensesOfUser(Guid userId);

        /// <summary>
        /// Retrieves all expenses.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ExpenseResponse>> GetAllExpenses();

        /// <summary>
        /// Deletes an existing expense.
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public Task<bool> DeleteExpense(Guid expenseId);

        /// <summary>
        /// Sort the expenses
        /// </summary>
        /// <param name="expenseResponses"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public Task<IEnumerable<ExpenseResponse>> SortExpenses(IEnumerable<ExpenseResponse> expenseResponses, string? sortBy, SortOrder? sortOrder);
    }
}
