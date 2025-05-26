using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.Domain.Repository_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repository_Classes
{
    public class ExpensesRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _db;

        public ExpensesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        ///  Create an Expense
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public async Task<Expense> CreateExpense(Expense expense)
        {
            await _db.Expenses.AddAsync(expense);
            await _db.SaveChangesAsync();
            return expense;
        }

        /// <summary>
        ///  Delete an Expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteExpense(Guid expenseId)
        {
            Expense? expense = await this.GetExpenseById(expenseId);
            if (expense == null)
            {
                return false;
            }

            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get all the expenses
        /// </summary>
        /// <returns></returns>
        public async Task<List<Expense>?> GetAllExpenses()
        {
            List<Expense>? expenses = await _db.Expenses.Include("User").ToListAsync();
            return expenses;
        }

        /// <summary>
        /// Get an Expense By Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public async Task<Expense?> GetExpenseById(Guid expenseId)
        {
            Expense? expense = await _db.Expenses.Include("User").FirstOrDefaultAsync(e => e.ExpenseId == expenseId);
            return expense;
        }

        /// <summary>
        /// Get all the expenses of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Expense>?> GetExpensesOfUser(Guid userId)
        {
            List<Expense>? expenses = await _db.Expenses.Include("User").Where(e => e.UserId == userId).ToListAsync();
            return expenses;
        }
    }
}
