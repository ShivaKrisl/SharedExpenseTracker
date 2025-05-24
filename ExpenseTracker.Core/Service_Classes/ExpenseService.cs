using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.Core.ValidationHelpers;

namespace ExpenseTracker.Core.Service_Classes
{
    public class ExpenseService : IExpenseService
    {

        private List<Expense> _expenses;
        private readonly IUserService _userService;

        public ExpenseService(IUserService userService)
        {
            _expenses = new List<Expense>();
            _userService = userService;
        }

        // Add SortBy Date
        // Add FilterBy Date

        /// <summary>
        /// Creates a new Expense
        /// </summary>
        /// <param name="expenseRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ExpenseResponse> CreateExpense(ExpenseRequest expenseRequest)
        {
            if(expenseRequest == null)
            {
                throw new ArgumentNullException(nameof(expenseRequest),"Invalid Request");
            }

            bool isModelValid = ValidateModel.IsModelValid(expenseRequest);
            if (!isModelValid)
            {
                throw new ArgumentException(ValidateModel.Errors, nameof(expenseRequest));
            }

            // check if user exists
            UserResponse? userResponse = await _userService.GetUserById(expenseRequest.UserId);
            if (userResponse == null)
            {
                throw new ArgumentException("User Not Found!!", nameof(expenseRequest));
            }

            Expense expense = expenseRequest.ToExpense();
            expense.ExpenseId = Guid.NewGuid();
            expense.DateOfCreation = DateTime.UtcNow;
            
            _expenses.Add(expense);
            return expense.ToExpenseResponse();
        }

        /// <summary>
        /// Delete an Expense By Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> DeleteExpense(Guid expenseId)
        {
            if(expenseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(expenseId), "Invalid Id");
            }

            Expense? expense = _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);

            if (expense == null)
            {
                return false;
            }

            _expenses.Remove(expense);

            return true;
        }

        /// <summary>
        /// Retrieve all the expenses
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseResponse>> GetAllExpenses()
        {
            if (!_expenses.Any())
            {
                return new List<ExpenseResponse>();
            }

            var expenseResponses = _expenses.Select(e => e.ToExpenseResponse()).ToList();
            return expenseResponses;
        }

        /// <summary>
        /// Get an Expense By Id
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ExpenseResponse?> GetExpenseById(Guid expenseId)
        {
            if(expenseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(expenseId), "Invalid Id");
            }

            Expense? expense = _expenses.FirstOrDefault(e => e.ExpenseId == expenseId);

            if(expense == null)
            {
                return null;
            }
            return expense.ToExpenseResponse();
        }

        /// <summary>
        /// Get all the expenses of an User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<ExpenseResponse>> GetExpensesOfUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId), "Invalid Id");
            }

            UserResponse? userResponse = await _userService.GetUserById(userId);
            if (userResponse == null)
            {
                return new List<ExpenseResponse>();
            }

            var expenses = _expenses.Where(e => e.UserId == userId).Select(p => p.ToExpenseResponse()).ToList();
            return expenses;
        }

        /// <summary>
        /// Sort the Expenses
        /// </summary>
        /// <param name="expenseResponses"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseResponse>> SortExpenses(IEnumerable<ExpenseResponse> expenseResponses, string? sortBy, SortOrder? sortOrder)
        {
            if (!expenseResponses.Any())
            {
                return expenseResponses;
            }
            List<ExpenseResponse> sortedExpenses = new List<ExpenseResponse>();

            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = nameof(ExpenseResponse.DateOfCreation);
                
            }

            if(sortOrder == null)
            {
                sortOrder = SortOrder.DESCENDING;
            }

            sortedExpenses = (sortBy, sortOrder) switch
            {
                (nameof(ExpenseResponse.DateOfCreation), SortOrder.ASCENDING) =>
                    expenseResponses.OrderBy(e => e.DateOfCreation).ToList(),

                (nameof(ExpenseResponse.DateOfCreation), SortOrder.DESCENDING) =>
                    expenseResponses.OrderByDescending(e => e.DateOfCreation).ToList(),

                (nameof(ExpenseResponse.Reason), SortOrder.ASCENDING) =>
                    expenseResponses.OrderBy(e => e.Reason.ToLower()).ToList(),

                (nameof(ExpenseResponse.Reason), SortOrder.DESCENDING) =>
                    expenseResponses.OrderByDescending(e => e.Reason.ToLower()).ToList(),

                (nameof(ExpenseResponse.ExpenseType), SortOrder.ASCENDING) =>
                    expenseResponses.OrderBy(e => e.ExpenseType.ToLower()).ToList(),

                (nameof(ExpenseResponse.ExpenseType), SortOrder.DESCENDING) =>
                    expenseResponses.OrderByDescending(e => e.ExpenseType.ToLower()).ToList(),

                (nameof(ExpenseResponse.Amount), SortOrder.ASCENDING) =>
                    expenseResponses.OrderBy(e => e.Amount).ToList(),

                (nameof(ExpenseResponse.Amount), SortOrder.DESCENDING) =>
                    expenseResponses.OrderByDescending(e => e.Amount).ToList(),

                _ => expenseResponses.ToList()
            };

            return sortedExpenses;

        }
    }
}
