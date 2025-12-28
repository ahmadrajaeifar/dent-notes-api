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
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("income")]
        public IActionResult GetIncome()
        {
            var dentistId = User.GetUserId();
            var report = _reportService.GetDentistIncome(dentistId);
            return Ok(report);
        }
    }
}
