namespace Web_AppointmentSystem.MVC.UIExceptions;

public class InvalidTokenException:Exception
{
    public InvalidTokenException()
    {
    }

    public InvalidTokenException(string? message) : base(message)
    {
    }
}
