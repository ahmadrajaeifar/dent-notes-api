using AutoMapper;
using DentalClinic.Api.DTOs.Dentists;
using DentalClinic.Api.Repositories;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistController : ControllerBase
    {
        private readonly DentistService _dentistService;
        private readonly IMapper _mapper;

        public DentistController(
            DentistService dentistService,
            IMapper mapper)
        {
            _dentistService = dentistService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dentist = _dentistService.GetDentistById(id);
            if (dentist == null)
                return NotFound(new { Message = "کاربری با این مشخصات یافت نشد!" });

            var dto = _mapper.Map<DentistReadDto>(dentist);
            return Ok(dto);
        }

        [HttpPost("login")]
        public IActionResult Login(DentistLoginDto loginDto)
        {
            var dentist = _dentistService.Login(loginDto);

            if (dentist == null)
                return Unauthorized(new { Message = "نام کاربری یا رمز عبور اشتباه وارد شده است!" });
            
            var dto = _mapper.Map<DentistReadDto>(dentist);
            
            return Ok(dto);
        }

        [HttpPost("register")]
        public IActionResult Register(DentistCreateDto registerDto)
        {
            var dentist = _dentistService.Register(registerDto);
            var dto = _mapper.Map<DentistReadDto>(dentist);

            if (dentist == null)
                return BadRequest(new { Message = "ثبت نام ناموفق! نام کاربری یا آدرس ایمیل وارد شده قبلا استفاده شده است." });

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}
