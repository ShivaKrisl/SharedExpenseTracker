using ExpenseTracker.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Domain.Repository_Interfaces
{
    public interface IExpenseRepository
    {
        /// <summary>
        /// Add Expense to Db
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public Task<Expense> CreateExpense(Expense expense);

        /// <summary>
        /// Get an Expense By Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public Task<Expense?> GetExpenseById(Guid expenseId);

        /// <summary>
        /// Get all the Expenses of a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<Expense>?> GetExpensesOfUser(Guid userId);

        /// <summary>
        /// Get all the Expenses
        /// </summary>
        /// <returns></returns>
        public Task<List<Expense>?> GetAllExpenses();

        /// <summary>
        /// Delete an Expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public Task<bool> DeleteExpense(Guid expenseId);
    }
}
