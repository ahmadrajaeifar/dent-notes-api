using AutoMapper;
using DentalClinic.Api.DTOs.Common;
//using DentalClinic.Api.DTOs.Dentists;
using DentalClinic.Contracts.DTOs.Dentists;
using DentalClinic.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistController : ControllerBase
    {
        private readonly DentistService _dentistService;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public DentistController(
            DentistService dentistService,
            TokenService tokenService,
            IMapper mapper)
        {
            _dentistService = dentistService;
            _tokenService = tokenService;
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
            var dentist = _dentistService.ValidateUser(loginDto.Username, loginDto.Password);
            if (dentist == null)
                return Unauthorized(new ApiResponse<string>(
                    "نام کاربری یا رمز عبور اشتباه است"));

            var token = _tokenService.GenerateToken(dentist);
            var refreshToken = _dentistService.SetRefreshToken(dentist);

            return Ok(new
            {
                token,
                refreshToken
            });
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

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var dentist = _dentistService.GetDentistByUsername(dto.Username);
            if (dentist == null || dentist.RefreshToken != dto.RefreshToken || dentist.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized(new ApiResponse<string>("توکن منقضی شده یا نامعتبر است"));

            // توکن جدید بساز
            var token = _tokenService.GenerateToken(dentist);
            var refreshToken = _dentistService.SetRefreshToken(dentist);

            return Ok(new
            {
                token,
                refreshToken
            });
        }
    }
}
