namespace Web_AppointmentSystem.MVC.UIExceptions.Common
{
    public class ModelStateException : Exception
    {
        public string PropertyName { get; set; }
        public ModelStateException()
        {
        }

        public ModelStateException(string? message) : base(message)
        {
        }

        public ModelStateException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
