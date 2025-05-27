using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Authorize]
    //[ValidateAntiForgeryToken]
    public class SharedExpenseController : CustomBaseController
    {
        private readonly ISharedExpenseService _sharedExpenseService;

        private readonly IExpenseService _expenseService;

        public SharedExpenseController(ISharedExpenseService sharedExpenseService, IExpenseService expenseService)
        {
            _sharedExpenseService = sharedExpenseService;
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSharedExpenses()
        {
            try
            {
                var expenses = await _sharedExpenseService.GetSharedExpenses();
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("users/{userId:guid}")]
        public async Task<IActionResult> GetSharedExpensesOfUser(Guid userId)
        {
            try
            {
                var expenses = await _sharedExpenseService.GetSharedExpensesByUserId(userId);
                return Ok(expenses);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("id/{sharedExpenseId:guid}")]
        public async Task<IActionResult> LoadSharedExpense(Guid sharedExpenseId, [FromQuery] string? sortBy, SortOrder? sortOrder)
        {
            try
            {
                var expenses = await _sharedExpenseService.LoadSharedExpense(sharedExpenseId);
                if (!expenses.Any())
                {
                    return Ok(expenses);
                }
                expenses = await _expenseService.SortExpenses(expenses, sortBy, sortOrder); 
                return Ok(expenses);

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("id/{sharedExpenseId:guid}/users/{userId:guid}")]
        public async Task<IActionResult> DeleteSharedExpense(Guid sharedExpenseId, Guid userId)
        {
            try
            {
                bool isDeleted = await _sharedExpenseService.DeleteSharedExpense(sharedExpenseId, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSharedExpense([FromBody] SharedExpenseRequest sharedExpenseRequest)
        {
            try
            {
                var sharedExpense = await _sharedExpenseService.CreateSharedExpense(sharedExpenseRequest);
                return Ok(sharedExpense);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
