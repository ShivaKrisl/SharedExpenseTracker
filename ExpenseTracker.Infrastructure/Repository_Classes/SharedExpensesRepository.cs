using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.Domain.Repository_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ExpenseTracker.Infrastructure.Repository_Classes
{
    public class SharedExpensesRepository : ISharedExpenseRepository
    {
        private readonly ApplicationDbContext _db;

        public SharedExpensesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add a Shared Expense to Db
        /// </summary>
        /// <param name="sharedExpense"></param>
        /// <returns></returns>
        public async Task<SharedExpense> CreateSharedExpense(SharedExpense sharedExpense)
        {
            await _db.AddAsync(sharedExpense);
            await _db.SaveChangesAsync();
            return sharedExpense;
        }

        /// <summary>
        /// Delete a SharedExpense
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSharedExpense(Guid sharedExpenseId)
        {
            SharedExpense? sharedExpense = await this.GetSharedExpenseById(sharedExpenseId);
            if (sharedExpense == null)
            {
                return false;
            }
            _db.SharedExpenses.Remove(sharedExpense);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get a Shared Expense By Id
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        public async Task<SharedExpense?> GetSharedExpenseById(Guid sharedExpenseId)
        {
            SharedExpense? sharedExpense = await _db.SharedExpenses.Include("CreatedByUser").FirstOrDefaultAsync(s => s.SharedExpenseId == sharedExpenseId);
            return sharedExpense;
        }

        /// <summary>
        /// Get a Shared Expense By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SharedExpense?> GetSharedExpenseByName(string name)
        {
            SharedExpense? sharedExpense = await _db.SharedExpenses.Include("CreatedByUser").FirstOrDefaultAsync(s => s.SharedExpenseName.ToLower() == name.ToLower());
            return sharedExpense;
        }

        /// <summary>
        /// Get all Shared Expenses
        /// </summary>
        /// <returns></returns>
        public async Task<List<SharedExpense>?> GetSharedExpenses()
        {
            List<SharedExpense>? sharedExpenses = await _db.SharedExpenses.Include("CreatedByUser").ToListAsync();
            return sharedExpenses;
        }

        /// <summary>
        /// Get all Shared Expenses of an User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SharedExpense>?> GetSharedExpensesOfUser(Guid userId, string email)
        {
            // need to fix this (fetching all from memory -- huge)
            // instead do Norm 
            var allExpenses = await _db.SharedExpenses
        .Include("CreatedByUser")
        .ToListAsync();

            return allExpenses
                .Where(s => s.CreatedByUserId == userId || s.UserIds.Contains(email))
                .ToList();
        }
    }
}
