using AutoMapper;
using DentalClinic.Api.Data;
using DentalClinic.Api.DTOs.Common;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DentalClinic.Api.Controllers
{
    [Authorize(Policy = "RequireDentistRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(
            PatientService patientService,
            IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPatients([FromQuery] PaginationParams pagination)
        {
            int dentistId = User.GetUserId();
            
            var (patients, totalCount) = 
                _patientService.GetPatientsByDentistWithCount(dentistId, pagination);

            var result = _mapper.Map<IEnumerable<PatientReadDto>>(patients);

            var meta = new PaginationMeta
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount
            };

            return Ok(new ApiResponse<IEnumerable<PatientReadDto>>(
                result, 
                meta,
                "لیست بیماران با موفقیت دریافت شد"));
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            var dentisId = User.GetUserId();
            var patient = _patientService.GetPatientById(id, dentisId);

            if (patient == null)
                return NotFound(new ApiResponse<string>(
                    "چنین بیماری ثبت نشده است!"));

            var dto = _mapper.Map<PatientReadDto>(patient);

            return Ok(new ApiResponse<PatientReadDto>(dto));
        }

        [HttpGet("search")]
        public IActionResult SearchPatients(
            [FromQuery] PatientSearchDto searchDto,
            [FromQuery] PaginationParams pagination)
        {
            var dentistId = User.GetUserId();

            var patients = _patientService.SearchPatients(
                searchDto,
                pagination,
                dentistId);

            var result = _mapper.Map<List<PatientReadDto>>(patients);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreatePatient(PatientCreateDto dto)
        {
            int dentistId = User.GetUserId();
            var patient = _patientService.CreatePatient(dto, dentistId);
            var result = _mapper.Map<PatientReadDto>(patient);

            return CreatedAtAction(
                nameof(GetPatientById), 
                new { id = patient.Id }, 
                new ApiResponse<PatientReadDto>(result, 
                    "اطلاعات بیمار با موفقیت ثبت شد"));
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, PatientUpdateDto dto)
        {
            var dentistId = User.GetUserId();
            var patient = _patientService.UpdatePatient(id, dto, dentistId);

            if (patient == null)
                return NotFound(new { message = "چنین بیماری ثبت نشده است!" });

            var result = _mapper.Map<PatientReadDto>(patient);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var dentistId = User.GetUserId();
            var result = _patientService.DeletePatient(id, dentistId);

            if (!result)
                return NotFound(new ApiResponse<string>("چنین بیماری ثبت نشده است!"));

            return Ok(new ApiResponse<string>("بیمار با موفقیت حذف شد"));
        }
    }
}
