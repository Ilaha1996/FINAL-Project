namespace Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;

public class EntityNotFoundException : Exception
{
    public int StatusCode { get; set; }
    public EntityNotFoundException()
    {

    }
    public EntityNotFoundException(string? message) : base(message)
    {

    }

    public EntityNotFoundException(int statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}
