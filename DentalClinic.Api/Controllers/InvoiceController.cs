using DentalClinic.Api.Data;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Authorize(Policy = "RequireDentistRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{patientId}")]
        public IActionResult GetInvoice(int patientId)
        {
            int dentistId = User.GetUserId();

            var invoice = _invoiceService.GenerateInvoice(patientId, dentistId);
            if (invoice == null)
                return NotFound("بیمار یافت نشد");

            return Ok(invoice);
        }
    }
}
