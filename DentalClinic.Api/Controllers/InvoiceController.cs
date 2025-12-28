using AutoMapper;
using DentalClinic.Api.Data;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Invoices;
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
        private readonly IMapper _mapper;

        public InvoiceController(
            InvoiceService invoiceService,
            IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpPost("{patientId}")]
        public IActionResult CreateInvoice(int patientId, 
            [FromBody] List<InvoiceItemCreateDto> items)
        {
            if (items == null || !items.Any())
                return BadRequest("حداقل یک خدمت باید ثبت شود");

            var invoice = _invoiceService.CreateInvoice(patientId, items);
            var dto = _mapper.Map<InvoiceReadDto>(invoice);

            return Ok(new ApiResponse<InvoiceReadDto>(dto));
        }

        [HttpPost("{invoiceId}/items")]
        public IActionResult AddService(
            int invoiceId,
            AddServiceToInvoiceDto dto)
        {
            var item = _invoiceService.AddServiceToInvoice(
                invoiceId,
                dto.DentalServiceId,
                dto.Price);

            return Ok(new ApiResponse<object>(item));
        }

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(int invoiceId)
        {
            var invoice = _invoiceService.GetInvoice(invoiceId);
            if (invoice == null)
                return NotFound(new ApiResponse<string>("صورتحساب یافت نشد!"));

            var dto = _mapper.Map<InvoiceReadDto>(invoice);
            return Ok(new ApiResponse<InvoiceReadDto>(dto));
        }
    }
}
