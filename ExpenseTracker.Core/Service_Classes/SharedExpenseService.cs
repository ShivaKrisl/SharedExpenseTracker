using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.Domain.Repository_Interfaces;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.Core.ValidationHelpers;


namespace ExpenseTracker.Core.Service_Classes
{
    public class SharedExpenseService : ISharedExpenseService
    {
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private readonly ISharedExpenseRepository _sharedExpenseRepository;

        public SharedExpenseService(IUserService userService, IExpenseService expenseService, ISharedExpenseRepository sharedExpenseRepository)
        {
            _userService = userService;
            _sharedExpenseRepository = sharedExpenseRepository;
            _expenseService = expenseService;
        }

        // Future goal:
        /*
         * If Participant want to leave that sharedExpense => Update shared Expense by deleting that Id
         * in participants list
         */

        private async Task<bool> CheckIfUserIsValid(List<Guid> userIds)
        {
            if (!userIds.Any())
            {
                return false;
            }

            foreach (var id in userIds)
            {
                UserResponse? userResponse = await _userService.GetUserById(id);
                if (userResponse == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Creates new Shared Expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<SharedExpenseResponse> CreateSharedExpense(SharedExpenseRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Invalid Request");
            }

            bool isModelValid = ValidateModel.IsModelValid(request);
            if (!isModelValid)
            {
                throw new ArgumentException(ValidateModel.Errors, nameof(request));
            }

            // check if sharedExpenseName already exists
            var nameExits = await _sharedExpenseRepository.GetSharedExpenseByName(request.SharedExpenseName);

            if (nameExits != null) {
                throw new ArgumentException("Name is already taken!!", nameof(request.SharedExpenseName));
            }

            // check if createdByUser exists
            UserResponse? userResponse = await _userService.GetUserById(request.CreatedByUserId);
            if (userResponse == null)
            {
                throw new ArgumentException("User Not Found", nameof(request));
            }

            // check if added users are unique
            HashSet<Guid> users = new HashSet<Guid>(request.UserIds);
            if (users.Count != request.UserIds.Count)
            {
                throw new ArgumentException("Duplicate Users found!!", nameof(request.UserIds));
            }

            // check if created by user added himself
            if (users.Contains(request.CreatedByUserId))
            {
                throw new ArgumentException("You cannot add yourself!!", nameof(request.CreatedByUserId));
            }

            // check if all the added Users exists
            bool validUsers = await this.CheckIfUserIsValid(request.UserIds);
            if (!validUsers)
            {
                throw new ArgumentException("One or More User doesnot exits!!", nameof(request.UserIds));
            }

            var sharedExpense = request.ToSharedExpense();
            sharedExpense.SharedExpenseId = Guid.NewGuid();

            sharedExpense = await _sharedExpenseRepository.CreateSharedExpense(sharedExpense);

            return sharedExpense.ToSharedExpenseResponse();

        }

        /// <summary>
        /// Delete Shared Expense 
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> DeleteSharedExpense(Guid sharedExpenseId, Guid userId)
        {
            if (sharedExpenseId == Guid.Empty)
            {
                throw new ArgumentNullException("Invalid Id", nameof(sharedExpenseId));
            }

            var sharedExpense = await _sharedExpenseRepository.GetSharedExpenseById(sharedExpenseId);
            if (sharedExpense == null)
            {
                return false;
            }

            if (sharedExpense.CreatedByUserId != userId)
            {
                throw new ArgumentException("Only creater has permission to delete this!!", nameof(userId));
            }

            bool isDeleted = await _sharedExpenseRepository.DeleteSharedExpense(sharedExpenseId);
            return true;
        }

        /// <summary>
        /// Get shared Expense by id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<SharedExpenseResponse>?> GetSharedExpenses()
        {
            var sharedExpenses = await _sharedExpenseRepository.GetSharedExpenses();
            if (sharedExpenses == null || sharedExpenses.Count == 0)
            {
                return new List<SharedExpenseResponse>();
            }
            return sharedExpenses.Select(s => s.ToSharedExpenseResponse()).ToList();
        }

        /// <summary>
        /// Get shared Expensed of User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<SharedExpenseResponse>> GetSharedExpensesByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId), "Invalid Id");
            }

            // check if user exists
            UserResponse? userResponse = await _userService.GetUserById(userId);
            if (userResponse == null)
            {
                throw new ArgumentException("User doesnot exists!!", nameof(userId));
            }

            var sharedExpenses = await _sharedExpenseRepository.GetSharedExpensesOfUser(userId);
            if(sharedExpenses == null)
            {
                return new List<SharedExpenseResponse>();
            }
            return sharedExpenses.Select(s => s.ToSharedExpenseResponse()).ToList();
        }

        /// <summary>
        /// Get all Users Expenses of a shared Expense
        /// </summary>
        /// <param name="sharedExpenseId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<ExpenseResponse>?> LoadSharedExpense(Guid sharedExpenseId)
        {
            
            if(sharedExpenseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(sharedExpenseId), "Invalid Id");
            }

            SharedExpense? sharedExpense = await _sharedExpenseRepository.GetSharedExpenseById(sharedExpenseId);
            if(sharedExpense == null)
            {
                return new List<ExpenseResponse>();
            }

            List<ExpenseResponse> expenseResponses = new List<ExpenseResponse>();

            expenseResponses.AddRange(await _expenseService.GetExpensesOfUser(sharedExpense.CreatedByUserId));
            foreach(var id in sharedExpense.UserIds)
            {
                expenseResponses.AddRange(await _expenseService.GetExpensesOfUser(id));
            }

            return expenseResponses;
        }
    }
}
