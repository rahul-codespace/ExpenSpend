using AutoMapper;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain.DTOs.Payments;
using ExpenSpend.Domain.Models.Payments;
using ExpenSpend.Repository.Contracts;
using ExpenSpend.Service.Contracts;
using ExpenSpend.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Service
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public PaymentAppService(IRepository<Payment> paymentRepository, ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _paymentRepository = paymentRepository;
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<Response> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return new Response(_mapper.Map<List<GetPaymentDto>>(payments));
        }

        public async Task<Response> GetPaymentByIdAsync(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return new Response("Payment not found.");
            }
            return new Response(_mapper.Map<GetPaymentDto>(payment));
        }

        public async Task<Response> GetPaymentsByUserIdAsync(Guid userId)
        {
            var payments = await _paymentRepository.GetAllAsync();
            var userPayments = payments.Where(p => p.OwenedById == userId).ToList();
            return new Response(_mapper.Map<List<GetPaymentDto>>(userPayments));
        }

        public async Task<Response> CreatePaymentAsync(CreatePaymentDto input)
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);

            var payment = _mapper.Map<Payment>(input);
            payment.IsSettled = false;
            payment.CreatedAt = DateTime.Now;
            payment.CreatedBy = currUser?.Id;

            await _paymentRepository.InsertAsync(payment);
            return new Response(payment);
        }

        public async Task<Response> UpdatePaymentAsync(Guid id, UpdatePaymentDto input)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return new Response("Payment not found.");
            }

            payment.Amount = input.Amount;
            payment.IsSettled = input.IsSettled;
            payment.ModifiedAt = DateTime.Now;

            await _paymentRepository.UpdateAsync(payment);
            return new Response(payment);
        }

        public async Task<Response> DeletePaymentAsync(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return new Response("Payment not found.");
            }

            await _paymentRepository.DeleteAsync(payment);
            return new Response("Payment deleted successfully.");
        }

        public async Task<Response> SettlePaymentAsync(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return new Response("Payment not found.");
            }

            payment.IsSettled = true;
            payment.ModifiedAt = DateTime.Now;

            await _paymentRepository.UpdateAsync(payment);
            return new Response(payment);
        }

    }
}
