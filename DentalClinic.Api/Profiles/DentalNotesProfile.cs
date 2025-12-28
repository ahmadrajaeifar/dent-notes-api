using AutoMapper;
using DentalClinic.Contracts.DTOs.Dentists;
using DentalClinic.Api.DTOs.Invoices;
using DentalClinic.Api.DTOs.Patients;
using DentalClinic.Api.DTOs.Payments;
using DentalClinic.Api.Entities;

namespace DentalClinic.Api.Profiles
{
    public class DentalNotesProfile : Profile
    {
        public DentalNotesProfile()
        {
            // Model -> DTO
            CreateMap<Patient, PatientReadDto>();
            CreateMap<Patient, PatientDebtDto>();
            CreateMap<Payment, PaymentReadDto>();
            CreateMap<Dentist, DentistReadDto>();
            CreateMap<DentistLoginResultDto, DentistReadDto>();
            CreateMap<Invoice, InvoiceReadDto>()
                .ForMember(d => d.TotalAmount,
                    o => o.MapFrom(s => s.TotalAmount))
                .ForMember(d => d.PaidAmount,
                    o => o.MapFrom(s => s.PaidAmount))
                .ForMember(d => d.RemainingAmount,
                    o => o.MapFrom(s => s.RemainingAmount));

            //DTO -> Model
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();
            CreateMap<DentistCreateDto, Dentist>();
            CreateMap<DentistUpdateDto, Dentist>();
        }
    }
}
