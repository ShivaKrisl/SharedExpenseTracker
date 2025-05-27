using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Authorize]
    //[ValidateAntiForgeryToken]
    public class ExpensesController : CustomBaseController
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllExpenses([FromQuery] string? sortBy, SortOrder? sortOrder)
        {
            try
            {
                var expenses = await _expenseService.GetAllExpenses();
                expenses = await _expenseService.SortExpenses(expenses, sortBy, sortOrder);
                return Ok(expenses);

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("id/{expenseId:guid}")]
        public async Task<IActionResult> GetExpenseById(Guid expenseId)
        {
            try
            {
                var expense = await _expenseService.GetExpenseById(expenseId);
                if (expense == null)
                {
                    return NotFound("Expense Not Found!!");
                }
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("users/{userId:guid}")]
        public async Task<IActionResult> GetExpensesOfUser(Guid userId, [FromQuery] string? sortBy, SortOrder? sortOrder)
        {
            try
            {
                var expenses = await _expenseService.GetExpensesOfUser(userId);
                expenses = await _expenseService.SortExpenses(expenses, sortBy, sortOrder);
                return Ok(expenses);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{expenseId:guid}")]
        public async Task<IActionResult> DeleteExpense(Guid expenseId)
        {
            try
            {
                bool isDeleted = await _expenseService.DeleteExpense(expenseId);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseRequest expenseRequest)
        {
            try
            {
                var expense = await _expenseService.CreateExpense(expenseRequest);
                return CreatedAtAction(nameof(GetExpenseById), new { expenseId = expense.ExpenseId }, expense);
            }catch(Exception ex) {
                return Problem(ex.Message);
            }
        }
    }
}
