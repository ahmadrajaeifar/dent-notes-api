using AutoMapper;
using DentalClinic.Api.Data;
using DentalClinic.Api.DTOs.Notes;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Authorize(Policy = "RequireDentistRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientNoteController : ControllerBase
    {
        private readonly PatientNoteService _service;
        private readonly IMapper _mapper;

        public PatientNoteController(
            PatientNoteService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddNote(
            int patientId,
            PatientNoteCreateDto dto)
        {
            var dentistId = User.GetUserId();

            var note = _service.AddNote(
                patientId,
                dentistId,
                dto.Note);

            return Ok(_mapper.Map<PatientNoteReadDto>(note));
        }

        [HttpGet]
        public IActionResult GetNotes(int patientId)
        {
            var dentistId = User.GetUserId();

            var notes = _service.GetNotes(patientId, dentistId);
            return Ok(_mapper.Map<List<PatientNoteReadDto>>(notes));
        }
    }
}
