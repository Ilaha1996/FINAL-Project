namespace Web_AppointmentSystem.MVC.UIExceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string? PropertyName { get; set; }
        public ApiException()
        {

        }

        public ApiException(string? message) : base(message)
        {

        }
    }
}
