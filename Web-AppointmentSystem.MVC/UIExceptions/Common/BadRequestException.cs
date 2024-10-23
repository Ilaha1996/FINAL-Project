namespace Web_AppointmentSystem.MVC.UIExceptions.Common
{
    public class BadRequestException : Exception
    {

        public BadRequestException()
        {
        }

        public BadRequestException(string? message) : base(message)
        {
        }

    }
}
