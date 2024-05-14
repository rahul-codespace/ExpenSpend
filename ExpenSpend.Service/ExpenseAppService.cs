using AutoMapper;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain.DTOs.Expenses;
using ExpenSpend.Domain.DTOs.Payments;
using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.Payments;
using ExpenSpend.Repository.Contracts;
using ExpenSpend.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Service
{
    public class ExpenseAppService : IExpenseAppService
    {
        private readonly IRepository<Expense> _expenseRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ExpenseAppService(
            IRepository<Expense> expenseRepository,
            ApplicationDbContext context, IMapper mapper,
            IHttpContextAccessor httpContext
        ){
            _expenseRepository = expenseRepository;
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Response> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return new Response(_mapper.Map<GetExpenseDto>(expenses));
        }

        public async Task<Response> GetExpenseByIdAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return new Response("Expense not found.");
            }
            return new Response(_mapper.Map<GetExpenseDto>(expense));
        }
        public async Task<Response> GetExpensePaymentsAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return new Response("Expense not found.");
            }

            var payments = await _context.Payments
                .Where(p => p.ExpenseId == id)
                .ToListAsync();

            return new Response(_mapper.Map<List<GetPaymentDto>>(payments));
        }


        public async Task<Response> GetGroupExpensesAsync(Guid groupId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.GroupId == groupId)
                .ToListAsync();

            return new Response(_mapper.Map<List<GetExpenseDto>>(expenses));
        }


        public async Task<Response> GetUserExpensesAsync(Guid userId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PaidById == userId)
                .ToListAsync();

            return new Response(_mapper.Map<List<GetExpenseDto>>(expenses));
        }


        public async Task<Response> GetUserGroupExpensesAsync(Guid userId, Guid groupId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.PaidById == userId && e.GroupId == groupId)
                .ToListAsync();

            return new Response(_mapper.Map<List<GetExpenseDto>>(expenses));
        }

        public async Task<Response> CreateExpenseAsync(CreateExpenseDto createExpenseDto)
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            if (currUser == null)
            {
                return new Response("Current user not found.");
            }
            var expense = _mapper.Map<Expense>(createExpenseDto);
            expense.PaidById = currUser.Id;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _expenseRepository.InsertAsync(expense);
                    await _context.SaveChangesAsync();

                    var groupUsers = await _context.GroupMembers
                        .Where(gu => gu.GroupId == expense.GroupId)
                        .Include(gu => gu.User)
                        .ToListAsync();

                    var amountPerUser = expense.Amount / groupUsers.Count;
                    var payments = new List<Payment>();
                    foreach (var groupUser in groupUsers)
                    {
                        var payment = new Payment
                        {
                            ExpenseId = expense.Id,
                            OwenedById = groupUser.UserId,
                            Amount = amountPerUser
                        };
                        payments.Add(payment);
                    }

                    await _context.Payments.AddRangeAsync(payments);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new Response("Expense created successfully.");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new Response("An error occurred while creating the expense and payments. Transaction has been rolled back.");
                }
            }
        }

        public async Task<Response> UpdateExpenseAsync(Guid id, UpdateExpenseDto updateExpenseDto)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return new Response("Expense not found.");
            }

            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            if (currUser == null)
            {
                return new Response("Current user not found.");
            }

            if (expense.PaidById != currUser.Id)
            {
                return new Response("You are not authorized to update this expense.");
            }

            _mapper.Map(updateExpenseDto, expense);

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _expenseRepository.UpdateAsync(expense);
                    await _context.SaveChangesAsync();

                    var payments = await _context.Payments
                        .Where(p => p.ExpenseId == expense.Id)
                        .ToListAsync();

                    var groupUsers = await _context.GroupMembers
                        .Where(gu => gu.GroupId == expense.GroupId)
                        .Include(gu => gu.User)
                        .ToListAsync();

                    var amountPerUser = expense.Amount / groupUsers.Count;
                    foreach (var payment in payments)
                    {
                        payment.Amount = amountPerUser;
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new Response("Expense updated successfully.");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new Response("An error occurred while updating the expense and payments. Transaction has been rolled back.");
                }
            }
        }

        public async Task<Response> DeleteExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return new Response("Expense not found.");
            }

            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            if (currUser == null)
            {
                return new Response("Current user not found.");
            }

            if (expense.PaidById != currUser.Id)
            {
                return new Response("You are not authorized to delete this expense.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _expenseRepository.DeleteAsync(expense);
                    await _context.SaveChangesAsync();

                    var payments = await _context.Payments
                        .Where(p => p.ExpenseId == expense.Id)
                        .ToListAsync();

                    _context.Payments.RemoveRange(payments);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new Response("Expense deleted successfully.");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new Response("An error occurred while deleting the expense and payments. Transaction has been rolled back.");
                }
            }
        }

        public async Task<Response> SettleExpenseAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return new Response("Expense not found.");
            }

            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            if (currUser == null)
            {
                return new Response("Current user not found.");
            }

            if (expense.PaidById != currUser.Id)
            {
                return new Response("You are not authorized to settle this expense.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    expense.IsSettled = true;
                    await _expenseRepository.UpdateAsync(expense);
                    await _context.SaveChangesAsync();

                    var payments = await _context.Payments
                        .Where(p => p.ExpenseId == expense.Id)
                        .ToListAsync();

                    foreach (var payment in payments)
                    {
                        payment.IsSettled = true;
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new Response("Expense settled successfully.");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new Response("An error occurred while settling the expense and payments. Transaction has been rolled back.");
                }
            }
        }
    }
}