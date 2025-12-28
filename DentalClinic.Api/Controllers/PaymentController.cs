using AutoMapper;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Invoices;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.DTOs.Payments;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Authorize(Policy = "RequireDentistRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly PatientProcedureService _patientProcedureService;
        private readonly InvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public PaymentController(
            PaymentService paymentService,
            PatientProcedureService patientProcedureService,
            InvoiceService invoiceService,
            IMapper mapper)
        {
            _paymentService = paymentService;
            _patientProcedureService = patientProcedureService;
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpPost("{patientId}/invoice")]
        public IActionResult CreateInvoice(int patientId, [FromBody] List<InvoiceItemCreateDto> items)
        {
            var invoice = _invoiceService.CreateInvoice(patientId, items);
            return Ok(invoice);
        }

        [HttpPost("{invoiceId}")]
        public IActionResult AddPayment(
            int invoiceId,
            PaymentCreateDto dto)
        {
            var payment = _paymentService.AddPayment(
                invoiceId,
                dto.Amount,
                dto.Method,
                dto.Description);

            var result = _mapper.Map<PaymentReadDto>(payment);
            return Ok(new ApiResponse<object>(result));
        }

        [HttpGet("{patientId}/debt")]
        public IActionResult GetPatientDebt(int patientId)
        {
            var debt = _patientProcedureService.GetPatientDebt(patientId);
            var dto = _mapper.Map<PatientDebtDto>(debt);
            return Ok(dto);
        }

        [HttpGet("invoice/{invoiceId}")]
        public IActionResult GetPaymentsByInvoice(int invoiceId)
        {
            var invoice = _invoiceService.GetInvoice(invoiceId);
            var dto = _mapper.Map<InvoiceReadDto>(invoice);
            return Ok(dto);
        }
    }
}
