namespace DentalClinic.Api.Entities
{
    public class PatientService
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int DentalServiceId { get; set; }
        public DentalService DentalService { get; set; } = null!;

        public DateTime ServiceDate { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
