namespace Web_AppointmentSystem.MVC.UIExceptions.Common
{
    public class ModelNotFoundException:Exception
    {
        public ModelNotFoundException()
        {

        }

        public ModelNotFoundException(string? message) : base(message)
        {

        }
    }
}
