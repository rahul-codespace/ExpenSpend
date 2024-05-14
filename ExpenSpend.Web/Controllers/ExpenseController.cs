using AutoMapper;
using ExpenSpend.Domain.DTOs.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseAppService _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseAppService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            if (expenses.IsSuccess)
            {
                return Ok(expenses.Data);
            }
            return NotFound(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense.IsSuccess)
            {
                return Ok(expense.Data);
            }
            return NotFound(expense);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseDto input)
        {
            var result = await _expenseService.CreateExpenseAsync(input);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(Guid id, UpdateExpenseDto input)
        {
            var result = await _expenseService.UpdateExpenseAsync(id, input);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var result = await _expenseService.DeleteExpenseAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }
    }
}
