using DentalClinic.Api.Data;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Authorize(Policy = "RequireDentistRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientServiceController : ControllerBase
    {
        private readonly PatientProcedureService _service;

        public PatientServiceController(PatientProcedureService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddService(PatientServiceCreateDto dto)
        {
            int dentistId = User.GetUserId();

            var result = _service.AddService(dto, dentistId);
            if (result == null)
                return BadRequest("اطلاعات نامعتبر است");

            return Ok(result);
        }
    }
}
