using AutoMapper;
using DentalClinic.Api.DTOs.Dentists;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Profiles
{
    public class DentalNotesProfile: Profile
    {
        public DentalNotesProfile()
        {
            // Model -> DTO
            CreateMap<Patient, PatientReadDto>();
            CreateMap<Dentist, DentistReadDto>();
            CreateMap<DentistLoginResultDto, DentistReadDto>();

            //DTO -> Model
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();
            CreateMap<DentistCreateDto, Dentist>();
            CreateMap<DentistUpdateDto, Dentist>();
        }
    }
}
