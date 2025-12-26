namespace DentalClinic.Api.DTOs.Patients
{
    public class PatientServiceCreateDto
    {
        public int PatientId { get; set; }
        public int DentalServiceId { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}
