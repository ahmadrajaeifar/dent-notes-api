namespace DentalClinic.Api.Middlewares
{
    public class BusinessException: Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
