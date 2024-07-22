using AutoMapper;
using ExpenSpend.Domain.DTOs.Payments;
using ExpenSpend.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentAppService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentAppService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            if (payments.IsSuccess)
            {
                return Ok(payments.Data);
            }
            return NotFound(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment.IsSuccess)
            {
                return Ok(payment.Data);
            }
            return NotFound(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto input)
        {
            var result = await _paymentService.CreatePaymentAsync(input);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, UpdatePaymentDto input)
        {
            var result = await _paymentService.UpdatePaymentAsync(id, input);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }
    }
}
