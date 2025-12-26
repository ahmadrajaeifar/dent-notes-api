using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Invoices;
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

        public PaymentController(
            PaymentService paymentService,
            PatientProcedureService patientProcedureService,
            InvoiceService invoiceService)
        {
            _paymentService = paymentService;
            _patientProcedureService = patientProcedureService;
            _invoiceService = invoiceService;
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

            return Ok(new ApiResponse<object>(payment));
        }

        [HttpGet("{patientId}/debt")]
        public IActionResult GetDebt(int patientId)
        {
            var debt = _patientProcedureService.GetPatientDebt(patientId);
            return Ok(debt);
        }
    }
}
